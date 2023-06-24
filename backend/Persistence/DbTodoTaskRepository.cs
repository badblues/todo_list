using TodoList.Persistence.Interfaces;
using TodoList.Domain;


namespace TodoList.Persistence
{
    public class DbTodoTaskRepository : ITodoTaskRepository
    {
        private ApplicationContext _applicationContext;
        public DbTodoTaskRepository(ApplicationContext context)
        {
            _applicationContext = context;
        }

        public TodoTask? GetTodoTask(Guid id)
        {
            TodoTask? task = _applicationContext.Tasks.Find(id);
            return task;
        }
        public IEnumerable<TodoTask> GetTodoTasks()
        {
            List<TodoTask> list = _applicationContext.Tasks.ToList();
            return list;
        }

        public void CreateTodoTask(TodoTask task)
        {
            _applicationContext.Tasks.Add(task);
            _applicationContext.SaveChanges();
        }

        public void UpdateTodoTask(TodoTask task)
        {
            var res = _applicationContext.Tasks.SingleOrDefault(t => t.Id == task.Id);
            _applicationContext.Entry(res).CurrentValues.SetValues(task);
            _applicationContext.SaveChanges();
        }

        public void DeleteTodoTask(Guid id)
        {
            TodoTask? task = _applicationContext.Tasks.Find(id);
            if (task != null)
            {
                _applicationContext.Remove(task);
                _applicationContext.SaveChanges();
            }
        }

    }
}
