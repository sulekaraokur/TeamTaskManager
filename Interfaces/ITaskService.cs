using TeamTaskManager.API.Entities;

namespace TeamTaskManager.API.Interfaces;

public interface ITaskService
{
    //Şefin dışarıdan(Garson) alabileceği taleplerin listesi
    Task<IEnumerable<TaskItem>> GetTasksByProjectIdAsync(int projectId); //Belirli bir projeye ait tüm görevleri liste haline getirme kuralı
    Task<TaskItem> CreateTaskAsync(int projectId, string title, string description); //Yeni bir görev(TaskItem) ver, bunu sisteme ekleyip sana döndürür.
}