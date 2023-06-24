using Microsoft.EntityFrameworkCore;
using TodoList.Domain;

namespace TodoList.Persistence
{
    public class ApplicationContext : DbContext
    {
        public DbSet<TodoTask> Tasks { set; get; } = null!;
        public DbSet<User> Users { set; get; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
          :base(options)
        {
            Database.EnsureCreated();
        }
    }
}