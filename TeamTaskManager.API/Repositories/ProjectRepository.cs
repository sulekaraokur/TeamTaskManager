using Microsoft.EntityFrameworkCore;
using TeamTaskManager.API.Data;
using TeamTaskManager.API.Entities;
using TeamTaskManager.API.Interfaces;

namespace TeamTaskManager.API.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly AppDbContext _context;

    //yamağımıza mutfağın(veritabanının) anahtarını veriyoruz
    public ProjectRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Project>> GetAllProjectsAsync()
    {
        //Buzdolabına(veritabanına) git,
        //Projects rafındaki her şeyi liste olarak getir
        return await _context.Projects.ToListAsync();
    }

    public async Task<Project> AddProjectAsync(Project project)
    {
        //Yeni bir proje eklemek için, projeyi Projects rafına koy ve kaydet
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
        return project; //Eklenen projeyi geri döndür
    }

    public async Task DeleteProjectAsync(int id)
{
    var project = await _context.Projects.FindAsync(id);
    if (project != null)
    {
        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
    }
}

}

//veritabanı kodları(entity framework) sadece bu dosyanın içinde
//kalsın istiyoruz.Diğer katmanlar verilerin nasıl
//kaydedildiğini bilmek zorunda kalmasın.
