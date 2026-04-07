using Microsoft.EntityFrameworkCore;
using TeamTaskManager.API.Entities;

namespace TeamTaskManager.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; } //DbSet, Entity Framework Core'da bir tabloyu temsil eder. Bu durumda, "Users" tablosunu temsil eder ve User sınıfındaki özelliklere karşılık gelir.
    public DbSet<Project> Projects { get; set; } //DbSet, Entity Framework Core'da bir tabloyu temsil eder. Bu durumda, "Projects" tablosunu temsil eder ve Project sınıfındaki özelliklere karşılık gelir.
    public DbSet<TaskItem> TaskItems { get; set; } //DbSet, Entity Framework Core'da bir tabloyu temsil eder. Bu durumda, "TaskItems" tablosunu temsil eder ve TaskItem sınıfındaki özelliklere karşılık gelir.
    public DbSet<Comment> Comments { get; set; } //DbSet, Entity Framework Core'da bir tabloyu temsil eder. Bu durumda, "Comments" tablosunu temsil eder ve Comment sınıfındaki özelliklere karşılık gelir.

}

