using Microsoft.EntityFrameworkCore;
using Domain;
using Microsoft.Data.SqlClient;

namespace Persistence
{
    public class ApplicationContext : DbContext
    {
        private DbSet<TodoTask> Users { set; get; }
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString;
            connectionString = @"Data Source=;Initial Catalog=TODODB;User ID=; Password=; TrustServerCertificate=True";
            SqlConnection sqlConnection = new(connectionString);
            sqlConnection.Open();
            optionsBuilder.UseSqlServer(sqlConnection);
        }
    }
}