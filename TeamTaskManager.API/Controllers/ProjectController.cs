using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamTaskManager.API.DTOs;
using TeamTaskManager.API.Interfaces;

namespace TeamTaskManager.API.Controllers;

//[Authorize] etiketi sayesinde bu garson, elinde geçerli bir dijtal anahtarı(token)
//olmayan hiçbir müşterinin siparişini(isteklerini) mutfağa 
//iletmeyecek! Güvenlik devrede

[Authorize]
[ApiController]
[Route("api/[controller]")]

public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;

    //garsonumuz,projelerle ilgili siparişileri iletmek üzere
    //usta şefi(projectService) yanına çağırıyor

    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    //1.POST : Yeni proje ekleme uç noktası
    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectRequest request)
    {
        //garson sipariş fişindeki bilgiler(request.Name, request.Description)
        //alıp mutfaktaki şefe(ProjectService) iletiyor

        var newProject = await _projectService.CreateProjectAsync(request.Name, request.Description);

        //işlem başarılıysa eklenen projenin son halini müşteriye
        //(ekrana) geri döndürüyoruz

        return Ok(newProject); 
    }

    //2.GET: Tüm Projeleri listeleme uç noktası
    [HttpGet]
    public async Task<IActionResult> GetAllProjects()
    {
        //garson, şefe "Buzdolabındaki tüm projeleri getir" der
        var projects = await _projectService.GetAllAsync();

        //şef, garsona projeleri verir ve garson da müşteriye geri döndürür
        return Ok(projects);
    }

    //3.DELETE: Proje silme uç noktası
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        await _projectService.DeleteProjectAsync(id);
        return Ok(new { message = "Proje başarıyla silindi." });
    }

    

}