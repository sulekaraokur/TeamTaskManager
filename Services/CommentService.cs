using TeamTaskManager.API.DTOs;
using TeamTaskManager.API.Entities;
using TeamTaskManager.API.Interfaces;

namespace TeamTaskManager.API.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly ITaskRepository _taskRepository; //Görevin var olup olmadığını kontrol edecek

    public CommentService(ICommentRepository commentRepository, ITaskRepository taskRepository)
    {
        _commentRepository = commentRepository;
        _taskRepository = taskRepository;
    }

    public async Task<Comment?> AddCommentAsync(int taskId, int userId ,AddCommentRequest request)
    {
        //1.Kural : Önce böyle bir task var mı diye kontrol et
        var taskExists = await _taskRepository.GetTaskByIdAsync(taskId);
        if(taskExists == null)
        {
            return null; // olmayan göreve yorum yapılmaz
            //Garsona(controller) boş dön 404
        }

        //2.Yeni tabağı(entity) hazırla
        var newComment = new Comment
        {
            Content =request.Content,
            TaskItemId = taskId,
            UserId = userId
            //createdAt zaten otomatik olarak şu anki zamanı atayacak
        };

        //3.Aşçıya(repository) bunu kaydet de
        await _commentRepository.AddCommentAsync(newComment);

        //4.Hazırlanan tabağı garsona(controller) gönder
        return newComment;

    }

    public async Task<IEnumerable<Comment>> GetCommentsByTaskIdAsync(int taskId)
    {
        //yorumları getirmek için ekstra bir kurala gerek yok,direkt 
        //repodan isteyip controllera veriyoruz
        return await _commentRepository.GetCommentsByTaskIdAsync(taskId);
    }

}