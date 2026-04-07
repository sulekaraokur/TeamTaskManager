using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TeamTaskManager.API.DTOs;
using TeamTaskManager.API.Interfaces;

namespace TeamTaskManager.API.Controllers;

[Microsoft.AspNetCore.Components.Route("api/[controller]")]
[ApiController]
[Authorize]//Bu sınıftaki işlemleri yapmak için login yapmak zorunludur
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    //1.Post: Belirli bir göreve yorum ekle
    //URL örneği : POST /api/Commment/task/1
    [HttpPost("task/{taskId}")]
    public async Task<IActionResult> AddComment([FromRoute] int taskId,
    [FromBody] AddCommentRequest request )
    {
        //Tokenın içinden, o an giriş uapmış olan kullanıcının idsini (nameIdentifier) çıkarıyoruz
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if(string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString ,out int userId))
        {
            return Unauthorized(new { message = "Kullanıcı kimliği doğrulanamadı. Lütfen tekrar giriş yapın." });
        }

        //Controller siparişi görevin idsi ve kullanıcının idsi ile birlikte
        //service e iletiytor(şef)
        var comment = await _commentService.AddCommentAsync(taskId,userId,request);
   
        //Şef görevi bulamazsa
        if(comment == null)
        {
            return NotFound(new { message = "Yorum yapılmak istenen görev bulunamadı." });
        
        }

        //Başarılıysa eklenen yorumu user a gönder
        return Ok(comment);
    
    }

     // 2. GET: Belirli bir görevin yorumlarını listele
    // URL örneği: GET /api/Comment/task/1
    [HttpGet("task/{taskId}")]
    public async Task<IActionResult> GetComments([FromRoute] int taskId)
    {
        // Şeften o göreve ait tüm yorumları getirmesini istiyoruz
        var comments = await _commentService.GetCommentsByTaskIdAsync(taskId);
        
        return Ok(comments);
    }
}