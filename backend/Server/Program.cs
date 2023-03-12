using Persistence;
using Domain;

namespace Server
{
    public class Program
    {
        private static DbTodoTaskRepository? repository;
        public static void Main() {
            Console.WriteLine("STARTING PROGRAM...");
            repository = new DbTodoTaskRepository();
            TodoTask task1 = new TodoTask(1, 0, "TASK1", "DESCRIPTION1");
            repository.CreateTodoTask(task1);
            TodoTask task2 = repository.GetTask(1);
            Console.WriteLine($"id = {task2.Id}, userid = {task2.UserId}, title = {task2.Title}");
            repository.DeleteTodoTask(1);
        }
    }
}