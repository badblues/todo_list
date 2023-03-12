using Domain;
using Microsoft.Data.SqlClient;
using Persistence.Interfaces;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;

namespace Persistence
{
    public class DbTodoTaskRepository : IRepository
    {
        private readonly SqlConnection sqlConnection;
        public DbTodoTaskRepository()
        {
            string connectionString;
            connectionString = @"Data Source=;Initial Catalog=TODODB;User ID=; Password=; TrustServerCertificate=True";
            sqlConnection = new(connectionString);
            sqlConnection.Open();
        }

        public void CreateTodoTask(TodoTask task)
        {
            String date = task.CreationDate.ToString("yyyy-MM-d HH:mm:ss");
            Console.WriteLine(date);
            String query = $"INSERT INTO tasks(id, user_id, title, details, creation_date) " +
                $"values({task.Id}, {task.UserId}, '{task.Title}', '{task.Details}', '{date}');";
            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Close();
        }

        public void DeleteTodoTask(int id)
        {
            String query = $"DELETE FROM tasks WHERE id = {id};";
            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Close();
        }

        public TodoTask GetTask(int id)
        {
            String query = $"SELECT * FROM tasks WHERE id = {id};";
            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            int task_id = (int)reader.GetValue(0);
            int user_id = (int)reader.GetValue(1);
            string title = (string)reader.GetValue(2);
            string details = (string)reader.GetValue(3);
            TodoTask task = new(task_id, user_id, title, details);
            reader.Close();
            return task;
        }
        public IEnumerable<TodoTask> GetTasks()
        {
            throw new NotImplementedException();
        }

        public void UpdateTodoTask(TodoTask task)
        {
            throw new NotImplementedException();
        }
    }
}
