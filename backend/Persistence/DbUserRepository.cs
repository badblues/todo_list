using TodoList.Persistence.Interfaces;
using TodoList.Domain;


namespace TodoList.Persistence
{
    public class DbUserRepository : IUserRepository
    {
        private ApplicationContext _applicationContext;
        public DbUserRepository(ApplicationContext context)
        {
            _applicationContext = context;
        }

        public User? GetUser(Guid id)
        {
            User? user = _applicationContext.Users.Find(id);
            return user;
        }

        public User? GetUser(string email)
        {
            foreach (var user in _applicationContext.Users)
                if (user.Email == email)
                    return user;
            return null;
        }

        public IEnumerable<User> GetUsers()
        {
            List<User> list = _applicationContext.Users.ToList();
            return list;
        }

        public void CreateUser(User user)
        {
            _applicationContext.Users.Add(user);
            _applicationContext.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            var res = _applicationContext.Users.SingleOrDefault(t => t.Id == user.Id);
            _applicationContext.Entry(res).CurrentValues.SetValues(user);
            _applicationContext.SaveChanges();
        }

        public void DeleteUser(Guid id)
        {
            User? user = _applicationContext.Users.Find(id);
            if (user != null)
            {
                _applicationContext.Remove(user);
                _applicationContext.SaveChanges();
            }
        }

    }
}
