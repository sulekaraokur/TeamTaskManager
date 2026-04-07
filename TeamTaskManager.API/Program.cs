using Microsoft.EntityFrameworkCore;
using TeamTaskManager.API.Data;//AppDbContext sınıfını kullanmak için gerekli olan using ifadesi. Bu, AppDbContext sınıfının tanımlandığı namespace'i belirtir ve bu sınıfı Program.cs dosyasında kullanmamızı sağlar.
using Scalar.AspNetCore;
using TeamTaskManager.API.Interfaces;
using TeamTaskManager.API.Repositories;
using TeamTaskManager.API.Services; //Scalar API'lerini ASP.NET Core uygulamanıza entegre etmek için gerekli olan using ifadesi. Bu, Scalar API özelliklerini kullanarak API referanslarını oluşturmanıza ve sunmanıza olanak tanır.
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;
using FluentValidation;
using System.Reflection;
using TeamTaskManager.API.Middlewares;

var builder = WebApplication.CreateBuilder(args); //uygulamanın temelni oluşturuluyor ,  ayarları yapılandırmak için kullanılır.

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor | 
                               Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

//veritabanı bağlantısını sisteme ekliyoruz
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")?.Trim();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (connectionString != null && connectionString.Contains("render.com"))
    {
        options.UseNpgsql(connectionString);
    }
    else
    {
        options.UseSqlServer(connectionString);
    }

    // --- [BU SATIRI EKLE] ---
    // Model değişiklikleri uyarısını susturur, böylece uygulama ayağa kalkar
    options.ConfigureWarnings(w => w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
});
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(); //  OpenAPI belgelerini oluşturmak için gerekli servisleri ekler. Bu, API'nizin otomatik olarak belgelenmesini sağlar.Yeni kimlik

//AddScoped ile Ram içinde bir nesne oluşturulur 
//ve bu nesne, işlem bitince hafızadan silinir.
// RAM kullanımı verimliliği artar
//Yaptığımız bu değişiklik,eğer sistemde herhangi birisi
//Senden IUserRepository sözleşmesi istediğinde, ona 
//arka planda UserRepository sınıfından yeni bir nesne verecektir.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    // JSON'a çevirirken sonsuz döngüleri (Object Cycle) görmezden gel
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

//AddFluentValidationAutoValidation(): Garsonun(controller) gelen isteği
//otomatik olarak bu kurallardan geçirmesini sağlar

//AddValidatorsFromAssembly: sisteme"git ve projenin içindeki tüm validator
//sınıflarını tara bul ve kullanıma hazırla" der.
//Tek tek her validator'ı kaydetme zahmetinden kurtarır. 


builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<TeamTaskManager.API.Mappings.MappingProfile>());
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();
// Proje ekibini sisteme tanıtıyoruz
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectService, ProjectService>();
// Görevler (Tasks) için Aşçı ve Şefin sisteme kaydedilmesi
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();
//yorum comment modülü için bağımlılıkların(dependency injection) eklenmes
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ICommentService, CommentService>();
//comment controller çalışmaya başladığında sistemden ICommentService isteyecek
//Biz bu kodla sisteme biri senden ICommentService isterse ona
//arka planda CommentService sınıfını ver

//dijital anahtar oluşturmak

//Güvenlik Kontrolü:sisteme JWT kullanılacağını söylüyoruz
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true, // Anahtarı biz mi ürettik? (Kontrol et)
            ValidateAudience = true, // Anahtar bizim kullanıcılarımız için mi? (Kontrol et)
            ValidateLifetime = true, // Anahtarın süresi dolmuş mu? (Kontrol et)
            ValidateIssuerSigningKey = true, // İmza bizim gizli şifremizle mi atılmış? (Kontrol et)
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

    // 2. YETKİLENDİRME: Kimlik doğrulandıktan sonra yetkisi var mı diye bakacak sistem
    builder.Services.AddAuthorization();




var app = builder.Build();
app.UseForwardedHeaders();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try 
    {
        var context = services.GetRequiredService<AppDbContext>();
        await context.Database.MigrateAsync();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Migration Hatası: {ex.Message}");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();  //API'nizin OpenAPI belgelerini oluşturur ve sunar. Bu, API'nizin nasıl kullanılacağını açıklayan belgeler sağlar.Json formatında
    app.MapScalarApiReference();//tarayıcıda gördüğümüz renkli ve düzenli bir şekilde API referanslarını sunar. 
    // Bu, geliştiricilerin API'nizi daha kolay anlamasına ve kullanmasına yardımcı olur.  
}


app.UseCors("AllowAll");
//app.UseHttpsRedirection();

app.UseAuthentication(); // Kimlik Kontrolü (Sen kimsin?)
app.UseAuthorization();  // Yetki Kontrolü (Buraya girmeye iznin var mı?)

app.UseMiddleware<ExceptionHandlingMiddleware>();//should be upper of mapcontrollers.

app.MapControllers();


app.Run();


