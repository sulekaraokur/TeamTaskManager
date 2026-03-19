using Microsoft.AspNetCore.Mvc;
using TeamTaskManager.API.Interfaces;

namespace TeamTaskManager.API.Controllers;  

//[Route] kısmı API'nin adresini belirler
//"api/auth"adresinden ulaşacağız

[Route("api/[controller]")]
[ApiController] 
//Bu sınıfın bir API denetleyicisi olduğunu belirtir. 
//Bu, model bağlama, doğrulama ve 
//diğer API özelliklerini otomatik olarak sağlar.
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

   //Dependency Injection:
   //Garsonumuz, Usta Şefi (AuthService) mutfaktan çağırıyor
   
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    //Dışardan gelen POST(veri gönderme) isteklerini karşılar
    [HttpPost("register")] 
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        try
        {
            //Şefe siparişi iletiyoruz
            var result = await _authService.RegisterAsync(request.Username, request.Email, request.Password);
         //Kayıt başarılıysa, 200 OK yanıtı döner ve sonuç verisini içerir.
            return Ok(result);
        }
        catch (Exception ex)
        {
        //Kayıt başarısızsa, 400 Bad Request yanıtı döner ve hata mesajını içerir.
            return BadRequest(ex.Message); 
        }
    }

    [HttpPost("login")] //Bu, "api/auth/login" adresine POST isteği yapıldığında bu yöntemin çalışacağını belirtir.
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            //Şefe siparişi iletiyoruz
            var result = await _authService.LoginAsync(request.Email, request.Password);
            //Giriş başarılıysa, 200 OK yanıtı döner ve sonuç verisini içerir.
            return Ok(new { Message = result }); //Giriş başarılı mesajını JSON formatında döner.
        }
        catch (Exception ex)
        {
            //Giriş başarısızsa, 400 Bad Request yanıtı döner ve hata mesajını içerir.
            return BadRequest(ex.Message);
        }
    }
}

//DTO: Data Transfer Object
//Bu sınıflar, API'ye gelen veriyi temsil eder.
//kullanıcı adı şifre email , paket halinde almak için kullanılır
//record kelimesi c# ta sadece veri taşımak için kullanılan
//hafif ve modern sınıflardır. 

public record RegisterRequest(string Username, string Email, string Password);
public record LoginRequest(string Email, string Password);