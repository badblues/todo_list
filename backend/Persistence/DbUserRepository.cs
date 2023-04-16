using TodoList.Persistence.Interfaces;
using TodoList.Domain;


namespace TodoList.Persistence
{
    public class DbUserRepository : IUserRepository
    {
        private ApplicationContext db;
        public DbUserRepository(ApplicationContext context)
        {
            db = context;
        }

        public User? GetUser(Guid id)
        {
            User? user = db.Users.Find(id);
            return user;
        }

        public User? GetUser(string email)
        {
            foreach (var user in db.Users)
                if (user.Email == email)
                    return user;
            return null;
        }

        public IEnumerable<User> GetUsers()
        {
            List<User> list = db.Users.ToList();
            return list;
        }

        public void CreateUser(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            var res = db.Users.SingleOrDefault(t => t.Id == user.Id);
            db.Entry(res).CurrentValues.SetValues(user);
            db.SaveChanges();
        }

        public void DeleteUser(Guid id)
        {
            User? user = db.Users.Find(id);
            if (user != null)
            {
                db.Remove(user);
                db.SaveChanges();
            }
        }

    }
}
