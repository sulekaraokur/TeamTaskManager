using Microsoft.EntityFrameworkCore;
using TeamTaskManager.API.Data;//AppDbContext sınıfını kullanmak için gerekli olan using ifadesi. Bu, AppDbContext sınıfının tanımlandığı namespace'i belirtir ve bu sınıfı Program.cs dosyasında kullanmamızı sağlar.
using Scalar.AspNetCore;
using TeamTaskManager.API.Interfaces;
using TeamTaskManager.API.Repositories; //Scalar API'lerini ASP.NET Core uygulamanıza entegre etmek için gerekli olan using ifadesi. Bu, Scalar API özelliklerini kullanarak API referanslarını oluşturmanıza ve sunmanıza olanak tanır.


var builder = WebApplication.CreateBuilder(args); //uygulamanın temelni oluşturuluyor ,  ayarları yapılandırmak için kullanılır.


//veritabanı bağlantısını sisteme ekliyoruz
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(); //  OpenAPI belgelerini oluşturmak için gerekli servisleri ekler. Bu, API'nizin otomatik olarak belgelenmesini sağlar.Yeni kimlik

//AddScoped ile Ram içinde bir nesne oluşturulur 
//ve bu nesne, işlem bitince hafızadan silinir.
// RAM kullanımı verimliliği artar
//Yaptığımız bu değişiklik,eğer sistemde herhangi birisi
//Senden IUserRepository sözleşmesi istediğinde, ona 
//arka planda UserRepository sınıfından yeni bir nesne verecektir.
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();  //API'nizin OpenAPI belgelerini oluşturur ve sunar. Bu, API'nizin nasıl kullanılacağını açıklayan belgeler sağlar.Json formatında
    app.MapScalarApiReference();//tarayıcıda gördüğümüz renkli ve düzenli bir şekilde API referanslarını sunar. 
    // Bu, geliştiricilerin API'nizi daha kolay anlamasına ve kullanmasına yardımcı olur.  
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () => //Bu, "/weatherforecast" yoluna gelen GET isteklerini işleyen bir endpoint tanımlar.
//  İstek geldiğinde, belirtilen kod bloğu çalıştırılır ve hava durumunu tahmin eden bir dizi oluşturulur.
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
