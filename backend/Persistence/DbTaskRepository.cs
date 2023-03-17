using TodoList.Domain;
using TodoList.Persistence.Interfaces;
using System.Collections.Generic;


namespace TodoList.Persistence
{
    public class DbTaskRepository : ITaskRepository
    {
        private ApplicationContext db;
        public DbTaskRepository(ApplicationContext context)
        {
            db = context;
        }

        public Domain.Task? GetTask(Guid id)
        {
            Domain.Task? task = db.Tasks.Find(id);
            return task;
        }
        public IEnumerable<Domain.Task> GetTasks()
        {
            List<Domain.Task> list = db.Tasks.ToList();
            return list;
        }

        public void CreateTask(Domain.Task task)
        {
            db.Tasks.Add(task);
            db.SaveChanges();
        }

        public void UpdateTask(Domain.Task task)
        {
            var res = db.Tasks.SingleOrDefault(t => t.Id == task.Id);
            db.Entry(res).CurrentValues.SetValues(task);
            db.SaveChanges();
        }

        public void DeleteTask(Guid id)
        {
            Domain.Task? task = db.Tasks.Find(id);
            if (task != null)
            {
                db.Remove(task);
                db.SaveChanges();
            }
        }

    }
}
