using Microsoft.EntityFrameworkCore;
using ToDoListAPI.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Using a renamed Task class to avoid confusion with System.Threading.Tasks.Task
    public DbSet<ToDoTask> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ToDoTask>().HasKey(t => t.Id); // Explicitly set Id as the primary key
    }
}
