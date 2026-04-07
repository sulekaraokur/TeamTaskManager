using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TeamTaskMaager.API.DTOs;
using TeamTaskManager.API.DTOs;
using TeamTaskManager.API.Entities;
using TeamTaskManager.API.Interfaces;

namespace TeamTaskManager.API.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;

    private readonly IMapper _mapper; // Fotokopi makinemiz
    
    //Şef, veritabanı işlemlerini kendisi yapmaz
    //Mutfaktaki aşçıyı(repository) yanına çağırır

    public TaskService(ITaskRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TaskItem>> GetTasksByProjectIdAsync(int projectId, PaginationFilter filter)
    {
        //Şef, "Bana bu projenin görevlerini getir" diyerek
        //işi doğrudan aşçıya(repository) paslıyor.
       return await _taskRepository.GetTaskByProjectIdAsync(projectId, filter);
    
    }
    public async Task<TaskItem> CreateTaskAsync(CreateTaskRequest request)
    {
      //eski uzun kodlar yerine:
     //DTO içinden request al , otomatik olarak bir göreve(TaskITem) dönüştür
     var newTask = _mapper.Map<TaskItem>(request);

     //Tarih gibi DTO'da olmayan,bizim belirlediğimiz otomatik alanları
     //elle ekleyebiliriz.
        newTask.CreatedAt = DateTime.UtcNow;

        return await _taskRepository.AddTaskAsync(newTask);//Aşçıya(repository) "Bunu fırına ver ve kaydet" de
       
        
    }

    public async Task<TaskItem?> UpdateTaskAsync(int id , UpdateTaskRequest request)
    {
        //1.Aşama tabağı yani taskı bul
        var existingTask = await _taskRepository.GetTaskByIdAsync(id);

        //2.Aşama:Kontrol et (Business Logic)
        if(existingTask == null)
        {
            return null;
            //Task yoksa boş dön
        }

        //3.Aşama: Updating or mapping
        existingTask.Title=request.Title;
        existingTask.Description=request.Description;
        existingTask.IsCompleted=request.IsCompleted;
        
        //4.Aşama: Aşçıya(Repository) "Bunu fırına ver ve kaydet " de
        await _taskRepository.UpdateTaskAsync(existingTask);
        //Bilgileri verileri güncelledik ama bu değişiklik henüz sadece bilg.
        //geçici hafızasında RAM de duruyor. bu komutla aşçıya(repos.) aşçıya Değişik.ç
        //veritabanına kalıcı olarak yaz talimatını veriyoruz(SaveChangesAsync)



        //5.Aşama: Son halini garsona(Controller) geri dönder
        return existingTask;

    }

    public async Task<bool> DeleteTaskAsync(int id)
    {
        //1.Taskı veritabanında bul
        var existingTask =  await _taskRepository.GetTaskByIdAsync(id);

        //2. Task yoksa(zaten silinmişse ya da hiç var olmamışsa )
        //false dön
        if(existingTask == null)
        {
            return false;
        }    

        //3.Task bulunduysa aşçıya(repository) bunu "sil" de
        await _taskRepository.DeleteTaskAsync(existingTask);

        //4.İşlem başarılı oldu mesajını controllera ilet
        return true;
    }


    public async Task<TaskItem?> AssignTaskAsync(int taskId, AssignTaskRequest request)
    {
        //1.Görevi bul
        var existingTask = await _taskRepository.GetTaskByIdAsync(taskId);

        //2.Görev yoksa null dön (Controller 404 dönecek)
        if(existingTask == null)
        {
            return null;
        }


        //3.Task üzerindeki "atanan kişi" etiketini yeni kullanıcı id si ile değiştir
        existingTask.AssignedUserId = request.UserId;

        //4.Aşcıya(repository) "bunu fırına ver ve kaydet " de
        await _taskRepository.UpdateTaskAsync(existingTask);

        //5.Güncellenmiş taskı controllere(garson) gönder
        return existingTask;
    }
 
}