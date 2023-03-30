using TodoList.Persistence.Interfaces;
using TodoList.Domain;


namespace TodoList.Persistence
{
    public class DbTodoTaskRepository : ITodoTaskRepository
    {
        private ApplicationContext db;
        public DbTodoTaskRepository(ApplicationContext context)
        {
            db = context;
        }

        public TodoTask? GetTodoTask(Guid id)
        {
            TodoTask? task = db.Tasks.Find(id);
            return task;
        }
        public IEnumerable<TodoTask> GetTodoTasks()
        {
            List<TodoTask> list = db.Tasks.ToList();
            return list;
        }

        public void CreateTodoTask(TodoTask task)
        {
            db.Tasks.Add(task);
            db.SaveChanges();
        }

        public void UpdateTodoTask(TodoTask task)
        {
            var res = db.Tasks.SingleOrDefault(t => t.Id == task.Id);
            db.Entry(res).CurrentValues.SetValues(task);
            db.SaveChanges();
        }

        public void DeleteTodoTask(Guid id)
        {
            TodoTask? task = db.Tasks.Find(id);
            if (task != null)
            {
                db.Remove(task);
                db.SaveChanges();
            }
        }

    }
}
