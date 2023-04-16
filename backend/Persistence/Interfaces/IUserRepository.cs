using TodoList.Domain;

namespace TodoList.Persistence.Interfaces
{
    public interface IUserRepository
    {
        User? GetUser(Guid id);
        User? GetUser(string email);
        IEnumerable<User> GetUsers();
        void CreateUser(User task);
        void UpdateUser(User task);
        void DeleteUser(Guid id);
    }
}
