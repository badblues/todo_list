using Domain;
using Microsoft.Data.SqlClient;
using Persistence.Interfaces;


namespace Persistence
{
    public class DbTodoTaskRepository : IRepository
    {
        private ApplicationContext db;
        public DbTodoTaskRepository(ApplicationContext context)
        {
            db = context;
        }

        public void CreateTodoTask(TodoTask task)
        {
            throw new NotImplementedException();
        }

        public void DeleteTodoTask(int id)
        {
            throw new NotImplementedException();
        }

        public TodoTask GetTask(int id)
        {
            throw new NotImplementedException();
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
