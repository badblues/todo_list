using TodoList.Domain;

namespace TodoList.Persistence.Interfaces
{
    public interface ITodoTaskRepository
    {
        TodoTask? GetTodoTask(Guid id);
        IEnumerable<TodoTask> GetTodoTasks();
        void CreateTodoTask(TodoTask task);
        void UpdateTodoTask(TodoTask task);
        void DeleteTodoTask(Guid id);
    }
}
