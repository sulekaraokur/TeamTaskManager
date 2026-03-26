using TeamTaskManager.API.Entities;

namespace TeamTaskManager.API.Interfaces;

public interface ITaskRepository
{
    //Bir proje idsi alır ve o projeye ait tüm görevlerin listeleneceğinin
    //sözleşmesini veren interface methodu
    Task<IEnumerable<TaskItem>> GetTaskByProjectIdAsync(int projectId); //Veritabanındaki belirli bir projeye ait tüm görevleri liste haline getirme kuralı
   //Yeni bir görev(TaskItem) ver, bunu sisteme ekleyip sana döndürür.
    Task<TaskItem> AddTaskAsync(TaskItem taskItem); //Veritabanına yeni bir görev ekleme kuralı
}
 
 //İşçimizin(repository) hangi yeteneklere sahip olması
 // gerektiğini belirliyoruz. şimdilik sadece belirli 
 // bir projeye ait tüm görevleri liste haline getirme 
 // ve yeni görev ekleme komutlarını verdik.


 //asenkron(eş zamanlı olmayan)