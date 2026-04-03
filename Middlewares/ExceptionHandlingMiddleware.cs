using System.Net;
using System.Text.Json;

namespace TeamTaskManager.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    //Bu sınıf, uygulama içinde meydana gelen istisnaları (exceptions) yakalayarak merkezi bir şekilde ele almak için kullanılan bir middleware'dir. İstisnaları loglar ve istemciye uygun bir hata mesajı döner.
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;


    //ExceptionHandlingMiddleware sınıfının yapıcı (constructor) metodu. Bu metod, middleware'in ihtiyaç duyduğu bağımlılıkları alır ve sınıfın alanlarına atar.
    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            //isteğin yoluna devam etmesini sağla 
            await _next(context); //Bir sonraki middleware'i çağırır. Eğer bu sırada bir istisna oluşursa, catch bloğuna geçilir.
        }
        catch (Exception ex)
        {
            //Hata oluşursa burası çalışır. 
            _logger.LogError(ex, "Beklenmedik bir hata oluştu."); //İstisna loglanır.
            await HandleExceptionAsync(context, ex); //İstisna işlenir ve istemciye uygun bir hata mesajı döner.
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json"; //İstemciye dönecek içeriğin JSON formatında olduğunu belirtir.
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; //HTTP durum

        var response = new
        {
          StatusCode=context.Response.StatusCode,
          Message="Beklenmedik bir hata oluştu. Lütfen teknik ekiple iletişime geçiniz.",
          Detail=exception.Message     //geliştirme aşamaında hatayı görmerk için
        };  

    return context.Response.WriteAsJsonAsync(response); //Hazırlanan hata mesajını JSON formatında istemciye döner.

    }
}