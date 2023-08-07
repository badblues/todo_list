using TodoList.Domain;
using TodoList.Persistence.Interfaces;


namespace TodoList.Persistence;

public class DbUserRepository : IUserRepository
{
    private ApplicationContext _applicationContext;
    public DbUserRepository(ApplicationContext context)
    {
        _applicationContext = context;
    }

    public User? Get(Guid id)
    {
        User? user = _applicationContext.Users.Find(id);
        return user;
    }

    public User? Get(string email)
    {
        foreach (var user in _applicationContext.Users)
            if (user.Email == email)
                return user;
        return null;
    }

    public IEnumerable<User> GetAll()
    {
        List<User> list = _applicationContext.Users.ToList();
        return list;
    }

    public void Create(User user)
    {
        _applicationContext.Users.Add(user);
        _applicationContext.SaveChanges();
    }

    public void Update(User user)
    {
        var res = _applicationContext.Users.SingleOrDefault(t => t.Id == user.Id);
        if (res != null) {
            _applicationContext.Entry(res).CurrentValues.SetValues(user);
            _applicationContext.SaveChanges();
        }
    }

    public void Delete(Guid id)
    {
        User? user = _applicationContext.Users.Find(id);
        if (user != null)
        {
            _applicationContext.Remove(user);
            _applicationContext.SaveChanges();
        }
    }

}

