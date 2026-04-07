using TeamTaskManager.API.Entities;

namespace TeamTaskManager.API.Interfaces;

public interface IProjectService
{
    //Tüm projeleri listeleme kuralı
    Task<IEnumerable<Project>> GetAllAsync();
    //Kullanıcıdan sadece projenin adını ve açıklamasını alacağız
    Task<Project> CreateProjectAsync(string name, string description);

    Task DeleteProjectAsync(int id);
}