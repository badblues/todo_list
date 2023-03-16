using Microsoft.EntityFrameworkCore;
using TodoList.Domain;

namespace TodoList.Persistence
{
    public class ApplicationContext : DbContext
    {
        private readonly string connectionString;
        public DbSet<Domain.Task> Tasks { set; get; }
        public ApplicationContext(string connectionString)
        {
            this.connectionString = connectionString;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}