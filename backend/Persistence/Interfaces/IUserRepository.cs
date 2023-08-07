using TodoList.Domain;

namespace TodoList.Persistence.Interfaces;

public interface IUserRepository : IRepository<User>
{
    User? Get(string email);
}
