using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        var newTask = await _taskService.CreateTaskAsync(request.ProjectId , request.Title , request.Description);

        //Şefin hazırladığğı yeni görevi müşteriye(ekrana endpoinst) sunuyor.
        return Ok(newTask);

    }

    //2.GET: Belirli bir projenin görevlerini listeleme
    //URL örneği : GET /api/Task/project/5 (5 numaralı projenin görevleri)

    [HttpGet("project/{projectId}")]
    public async Task<IActionResult> GetTasksByProjectId(int projectId)
    {
        var tasks = await _taskService.GetTasksByProjectIdAsync(projectId);
        return Ok(tasks);
    }
}

