using TeamTaskManager.API.DTOs;
using TeamTaskManager.API.Entities;

namespace TeamTaskManager.API.Interfaces;

public interface ICommentService
{
    //Yeni yorum ekleme(Görev yoksa null dönecek)
    Task<Comment?> AddCommentAsync(int taskId, int userId , AddCommentRequest request);

    //Görevin yorumlarını getirme
    Task<IEnumerable<Comment>> GetCommentsByTaskIdAsync(int taskId);
    
}