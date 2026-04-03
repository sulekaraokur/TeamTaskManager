using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamTaskMaager.API.DTOs;
using TeamTaskManager.API.DTOs;
using TeamTaskManager.API.Interfaces;

namespace TeamTaskManager.API.Controllers;

[Authorize] //Güvenlik kalkanı, sadece giriş yapmış(tokenı olan)
//kişiler görev ekleyebilir ya da görebilir.
[ApiController]
[Route("api/[controller]")]

public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    //Garson, görev siparişlerini iletmek için TaskService(şef) ile anlaşıyor
    public TaskController(ITaskService taskService)
    {
        _taskService =  taskService;
    }

    //1.POST: Yeni görev ekleme
    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequest request)
    {
        //Garson sipariş fişindeki(DTO) bilgileri alıp şefe(service) veriyor
        var newTask = await _taskService.CreateTaskAsync(request);

        //Şefin hazırladığğı yeni görevi müşteriye(ekrana endpoinst) sunuyor.
        return Ok(newTask);

    }

    //2.GET: Belirli bir projenin görevlerini listeleme
    //URL örneği : GET /api/Task/project/5 (5 numaralı projenin görevleri)

    [HttpGet("project/{projectId}")]
    public async Task<IActionResult> GetTasksByProjectId([FromRoute] int projectId, [FromQuery] PaginationFilter filter)
    {
        var tasks = await _taskService.GetTasksByProjectIdAsync(projectId,filter);
        return Ok(tasks);
    }

    //3.PUT : Mevcut Bir Görevi Güncelleme
    //URL örneği: PUT/api/Task/5 (5 numaralı görevi günceller)
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask([FromRoute] int id, [FromBody] UpdateTaskRequest request)
    {
        //Garson(controller) siparişi şefe(service) iletiyor
        var updatedTask = await _taskService.UpdateTaskAsync(id,request);

        //Şef eğer ben böyle bir task bulamadım derse null
        if(updatedTask==null)
        {
            //404 bulunamadı hatası dönecek
            return NotFound(new {message = "Güncellenmek istenen görev bulunamadı!"});
        }

        //şef tabağı başarıyla güncelleyip geri gönderdiyse, müşteriye 200 OK ile sunulan sunuyoruz.

        return Ok(updatedTask); 


    }

    //güncellemek için put komutu kullanılır.{id} ise dinamik bir değişkendir.
    
    //[FromRoute] ind id: c# a diyoruz ki : gerekli olan bu id bilgisini
    //müşterinin girdiği url adresinden çekip al

    //[FromBody] UpdateTaskRequest request:c# a diyoruz ki güncellenecek
    //yeni başlık, açıklama ve IsCompleted gibi bilgileri ise Urlden değil
    //paketin gövdesinden (JSON dosyasından)al

    //4.Delete :Mevcut bir görevi silme
    //URL örneği: DELETE /api/Task/1 (1 numaralı görevi siler)
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask([FromRoute] int id)
    {
        //Şefe(service) "Bu ID'li görevi sil" diyoruz ve cevabı 
        // true/false alıyoruz
        var isDeleted = await _taskService.DeleteTaskAsync(id);

        //Eğer şef(service) false dönerse(görev bulunamadıysa)
        if(!isDeleted)
        {
            return NotFound(new {message = "Silinmek istenen görev bulunamadı."});

        }

      //Görev başarıyla silindiyse 204 No content Döndür
      return NoContent();  
        
    }
    //5.PUT : Bir Göreve Kullanıcı Atama
    //URL örneği : PUT /api/Task/1/assign (1 numaralı göreve kullanıcı atar)
    [HttpPut("{id}/assign")]
    public async Task<IActionResult> AssignTask([FromRoute] int id, [FromBody] AssignTaskRequest request)
    {
        var assignedTask = await _taskService.AssignTaskAsync(id,request);

        if(assignedTask == null)
        {
            return NotFound(new {message = "Atama yapılmak istenen görev bulunamadı."});
        }

        return Ok(assignedTask);
    }
    
}

//IActionResult 404,204,200 döndürür bu Microsoft.AspNetCore.Mvc kütüph
//den gelir.