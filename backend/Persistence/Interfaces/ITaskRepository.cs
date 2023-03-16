using TodoList.Domain;

namespace TodoList.Persistence.Interfaces
{
    public interface ITaskRepository
    {
        Domain.Task? GetTask(Guid id);
        IEnumerable<Domain.Task> GetTasks();
        void CreateTask(Domain.Task task);
        void UpdateTask(Domain.Task task);
        void DeleteTask(Guid id);
    }
}
