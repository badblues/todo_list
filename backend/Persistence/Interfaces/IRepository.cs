using Domain;

namespace Persistence.Interfaces
{
    public interface IRepository
    {
        TodoTask? GetTask(int id);
        IEnumerable<TodoTask> GetTasks();
        void CreateTodoTask(TodoTask task);
        void UpdateTodoTask(TodoTask task);
        void DeleteTodoTask(int id);
    }
}
