using TeamTaskManager.API.Entities;
using TeamTaskManager.API.Interfaces;

namespace TeamTaskManager.API.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    
    //Şef, veritabanı işlemlerini kendisi yapmaz
    //Mutfaktaki aşçıyı(repository) yanına çağırır

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<IEnumerable<TaskItem>> GetTasksByProjectIdAsync(int projectId)
    {
        //Şef, "Bana bu projenin görevlerini getir" diyerek
        //işi doğrudan aşçıya(repository) paslıyor.
        return await _taskRepository.GetTaskByProjectIdAsync(projectId); 
    }

    public async Task<TaskItem> CreateTaskAsync(int projectId, string title, string description)
    {
        //İŞ KURALLARI BURADA İŞLETİLİR:
        //Dışardan sadece proje ID, başlık ve açıklama geldi
        //Ama veritabanının "Oluşturulma tarihi" ve "Tamamlandı mı?"
        //bilgilerine de ihtiyacı var.
        //Şef, bu eksik malzemeleri kendi ekleyip tabağı (TaskItem) hazırlıyor.

        var newTask = new TaskItem
        {
            ProjectId = projectId,
            Title = title,
            Description = description,
            // Görevin oluşturulduğu anı sistem saatiyle belirliyoruz
            CreatedAt = DateTime.UtcNow, 
            // Yeni görev doğal olarak henüz tamamlanmamıştır
            IsCompleted = false
        };

        //Şef, hazırladığı tam teşekküllü tabağı (TaskItem)
        //fırına vermesi(veritabanına kaydetmesi) için 
        //açşıya iletiyor.

        return await _taskRepository.AddTaskAsync(newTask);
    }
}