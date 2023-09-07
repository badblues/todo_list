using TodoList.Domain;
using TodoList.Persistence.Interfaces;

namespace TodoList.Persistence;
public class DbTodoTaskRepository : ITodoTaskRepository
{
    private readonly ApplicationContext _applicationContext;
    public DbTodoTaskRepository(ApplicationContext context)
    {
        _applicationContext = context;
    }

    public TodoTask? Get(Guid id)
    {
        TodoTask? task = _applicationContext.Tasks.Find(id);
        return task;
    }
    public IEnumerable<TodoTask> GetAll()
    {
        List<TodoTask> list = _applicationContext.Tasks.ToList();
        return list;
    }

    public void Create(TodoTask task)
    {
        _applicationContext.Tasks.Add(task);
        _applicationContext.SaveChanges();
    }

    public void Update(TodoTask task)
    {
        var res = _applicationContext.Tasks.SingleOrDefault(t => t.Id == task.Id);
        if (res != null)
        {
            _applicationContext.Entry(res).CurrentValues.SetValues(task);
            _applicationContext.SaveChanges();
        }
    }

    public void Delete(Guid id)
    {
        TodoTask? task = _applicationContext.Tasks.Find(id);
        if (task != null)
        {
            _applicationContext.Remove(task);
            _applicationContext.SaveChanges();
        }
    }

}

