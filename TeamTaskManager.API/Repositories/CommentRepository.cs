using Microsoft.EntityFrameworkCore;
using TeamTaskManager.API.Data;
using TeamTaskManager.API.Entities;
using TeamTaskManager.API.Interfaces;

namespace TeamTaskManager.API.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly AppDbContext _context;

    public CommentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddCommentAsync(Comment comment)
    {
        //Yorumlar rafına(tablosuna) yeni yorum ekle
        await _context.Comments.AddAsync(comment);
        //değişikliği veritabanına kalıcı olarak kaydet
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Comment>> GetCommentsByTaskIdAsync(int taskId)
    {
        //veritabanındaki tüm yorumlar arasından TaskItemId'si eşleşenleri bul
        return await _context.Comments
                    .Where(c => c.TaskItemId == taskId)
                    .OrderByDescending(c => c.CreatedAt)// En yeni yorum en üstte görünsün
                    .ToListAsync();
    }
}
