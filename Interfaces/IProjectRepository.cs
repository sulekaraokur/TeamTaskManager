using TeamTaskManager.API.Entities;

namespace TeamTaskManager.API.Interfaces;

public interface IProjectRepository
{
    //veritabanındaki tüm projeleri liste haline getirme kuralı
    Task<IEnumerable<Project>> GetAllProjectsAsync();

    //Veritabanına yeni bir proje ekleme kuralı
    Task<Project> AddProjectAsync(Project project);
}

//işçimizin(repository) hangi yeteneklere sahip olması gerektiğini
//belirliyoruz. şimdilik sadece hepsini getir ve yeni ekle
//komutlarını verdik.

