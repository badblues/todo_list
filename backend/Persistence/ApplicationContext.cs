using Microsoft.EntityFrameworkCore;
using Domain;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Collections.Specialized;

namespace Persistence
{
    public class ApplicationContext : DbContext
    {
        public DbSet<TodoTask> Tasks { set; get; }
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString;
            string? username = ConfigurationManager.AppSettings.Get("db_username");
            string? password = ConfigurationManager.AppSettings.Get("db_password");
            connectionString = @$"Data Source=;Initial Catalog=TODODB;User ID={username}; Password={password}; TrustServerCertificate=True";
            SqlConnection sqlConnection = new(connectionString);
            sqlConnection.Open();
            Console.WriteLine("CONNECTED TO MSQL SERVER.");
            optionsBuilder.UseSqlServer(sqlConnection);
        }
    }
}