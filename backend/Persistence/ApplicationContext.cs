using Microsoft.EntityFrameworkCore;
using TodoList.Domain;

namespace TodoList.Persistence
{
    public class ApplicationContext : DbContext
    {
        private readonly string _connectionString;
        public DbSet<TodoTask> Tasks { set; get; } = null!;
        public DbSet<User> Users { set; get; } = null!;

        public ApplicationContext(string connectionString)
        {
            this._connectionString = connectionString;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }
    }
}