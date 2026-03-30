using TeamTaskManager.API.DTOs;
using TeamTaskManager.API.Entities;

namespace TeamTaskManager.API.Interfaces;

public interface ITaskService
{
    //Şefin dışarıdan(Garson) alabileceği taleplerin listesi
    Task<IEnumerable<TaskItem>> GetTasksByProjectIdAsync(int projectId); //Belirli bir projeye ait tüm görevleri liste haline getirme kuralı
    Task<TaskItem> CreateTaskAsync(int projectId, string title, string description); //Yeni bir görev(TaskItem) ver, bunu sisteme ekleyip sana döndürür.
     //Dışarıdan gelen ID ve yeni bilgilerle görevi günceller
    Task<TaskItem?> UpdateTaskAsync(int id, UpdateTaskRequest request);



    //ID'si verilen görevi bulup siler. Başarılıysa true, bulamazsa false
    Task<bool> DeleteTaskAsync(int id);

    //Belirli bir göreve, belirli bir kullanıcıyı atar
    Task<TaskItem?> AssignTaskAsync(int taskId,AssignTaskRequest request);
    
    

}
//Task yazım ifadesi işlemin asenkron yani beklemeli yapacağını belirtir
//TaskItem? sonundaki soru işareti şunu söyler: Ben sana güncellenmiş 
//TaskItem ı geri döndürmeye çalışacağım ama ya o idye sahip bir görev
//veritabanında yoksa? o zaman sana null döndürebilirim. Hazırlıklı ol

//int id, UpdateTaskRequest request : metodun içine aldığı parametreler
//Yani şef(service) diyor ki: bana hani taskı (id) değiştireceğimi ve
//üzerindeki malzemeleri neyle değiştireceğimi(request) yani fişi ver

