using Microsoft.EntityFrameworkCore;
using TeamTaskMaager.API.DTOs;
using TeamTaskManager.API.Data;
using TeamTaskManager.API.Entities;
using TeamTaskManager.API.Interfaces;

namespace TeamTaskManager.API.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
        //AppDbContext'i kullanarak veritabanı işlemlerini gerçekleştireceğiz. 
        //Bu, veritabanına erişim sağlayan bir sınıftır
        //  ve genellikle Entity Framework Core tarafından sağlanır.

    }

    public async Task<IEnumerable<TaskItem>> GetTaskByProjectIdAsync(int projectId,PaginationFilter filter)
    {
       return await _context.TaskItems
            .Where(t => t.ProjectId == projectId)
            // Sayfalama Formülü: (Sayfa No - 1) * Sayfa Boyutu kadar kaydı ATLA (Skip)
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            // Kalanların içinden Sayfa Boyutu kadarını AL (Take)
            .Take(filter.PageSize)
            .ToListAsync();
    }

    public async Task<TaskItem> AddTaskAsync(TaskItem taskItem)
    {
        await _context.TaskItems.AddAsync(taskItem); //Veritabanına yeni bir görev ekleme kuralı
        await _context.SaveChangesAsync(); //Değişiklikleri veritabanına kaydetme işlemi
        return taskItem; //Eklenen görevi geri döndürme
    }

    public async Task<TaskItem?> GetTaskByIdAsync(int id)
    {
        //FindAsync, veritabanında sadece ID'ye(primary key) göre 
        // en hızlı arama yapan metottur

        return await _context.TaskItems.FindAsync(id);  
    }

    public async Task UpdateTaskAsync(TaskItem taskItem)
    {
        //görevi güncellemeye hazırla ve değişiklikleri kaydet
        _context.TaskItems.Update(taskItem);
        await _context.SaveChangesAsync();
    }


    public async Task DeleteTaskAsync(TaskItem taskItem)
    {
        //1.Görevi silinmek üzere işaretle
        //Burda yazdığımız TaskItems tabloyu temsil ediyor taskItem ise 
        //belirlediğimiz taskı temsil ediyor.
        _context.TaskItems.Remove(taskItem);

        //2.Değişikliği(silme işlemini) veritabanına kalıcı olarak kaydet
        await _context.SaveChangesAsync();
    }
    
    
}