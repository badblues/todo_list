using Domain;
using Persistence.Interfaces;
using System.Collections.Generic;


namespace Persistence
{
    public class DbTodoTaskRepository : IRepository
    {
        private ApplicationContext db;
        public DbTodoTaskRepository(ApplicationContext context)
        {
            db = context;
        }

        public void CreateTodoTask(TodoTask task)
        {
            try
            {
                db.Tasks.Add(task);
                db.SaveChanges();
            }
            catch (NullReferenceException ex)
            { 
                Console.WriteLine(ex.Message);
            }

        }

        public void DeleteTodoTask(int id)
        {
            try
            {
                TodoTask? task = db.Tasks.Find(id);
                if (task != null)
                {
                    db.Remove(task);
                    db.SaveChanges();
                }
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void DeleteTodoTask(TodoTask task)
        {
            try
            {
                db.Remove(task);
                db.SaveChanges();
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public TodoTask? GetTask(int id)
        {
            try
            {
                TodoTask? task = db.Tasks.Find(id);
                return task;
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public IEnumerable<TodoTask> GetTasks()
        {
            try
            {
                List<TodoTask> list = db.Tasks.ToList();
                return list;
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
                return new List<TodoTask>();
            }
        }

        public void UpdateTodoTask(TodoTask task)
        {
            try
            {
                var res = db.Tasks.SingleOrDefault(t => t.Id == task.Id);
                if (res != null)
                {
                    res.Completed = task.Completed;
                    res.Title = task.Title;
                    res.Details = task.Details;
                    res.EditDate = DateTime.Now;
                    db.SaveChanges();
                }
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
