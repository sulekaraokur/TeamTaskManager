using TeamTaskManager.API.Entities;

namespace TeamTaskManager.API.Interfaces;

public interface ICommentRepository
{
    //Yeni bir yorum eklemek için
    Task AddCommentAsync(Comment comment);

    //Belirli bir göreve ait tüm yorumları listelemek için
    Task<IEnumerable<Comment>> GetCommentsByTaskIdAsync(int taskId);

}