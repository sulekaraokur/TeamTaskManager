using TeamTaskManager.API.Entities;
using TeamTaskManager.API.Interfaces;

namespace TeamTaskManager.API.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;

    //şefimiz,projelerle ilgilenen yamağı(repository)
    //yanına çağırıyor.
    public ProjectService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<IEnumerable<Project>> GetAllAsync()
    {
        //şef kontrol eder, özel bir kural yoksa yamağa
        //hepsin getir
        //der
        return await _projectRepository.GetAllProjectsAsync();
    }

    public async Task<Project> CreateProjectAsync(string name, string description)
    {
        //1.İş Kuralı(Business Logic): Proje adı boş olamaz!
        if(string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Proje adı boş bırakılamaz!");
        }

        //2.Yeni Proje malzemesini hazırla
        var newProject = new Project
        {
            Name = name,
            Description = description,
            CreatedAt = DateTime.UtcNow//projenin oluşturulma tarihini tam şu an olarak ayarla

        };

        //3.Yamağa "Bunu veritabanına kaydet" emrini ver
        return await _projectRepository.AddProjectAsync(newProject);
         
    }   
}