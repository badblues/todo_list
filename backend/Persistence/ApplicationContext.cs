using Microsoft.EntityFrameworkCore;
using TodoList.Domain;

namespace TodoList.Persistence
{
    public class ApplicationContext : DbContext
    {
        private readonly string connectionString;
        public DbSet<TodoTask> Tasks { set; get; } = null!;
        public DbSet<User> Users { set; get; } = null!;

        public ApplicationContext(string connectionString)
        {
            this.connectionString = connectionString;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}