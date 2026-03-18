using Microsoft.EntityFrameworkCore;
using TeamTaskManager.API.Entities;

namespace TeamTaskManager.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } //DbSet, Entity Framework Core'da bir tabloyu temsil eder. Bu durumda, "Users" tablosunu temsil eder ve User sınıfındaki özelliklere karşılık gelir.
}

