using Domain;

namespace Persistence.Interfaces
{
    public interface IRepository
    {
        TodoTask GetTask(Guid id);
        IEnumerable<TodoTask> GetTasks();
        void CreateTodoTask(TodoTask task);
        void UpdateTodoTask(TodoTask task);
        void DeleteTodoTask(Guid id);
    }
}
