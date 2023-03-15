using Persistence;
using Domain;

namespace Server
{
    public class Program
    {
        private static DbTodoTaskRepository? repository;
        public static void Main() {
            Console.WriteLine("STARTING PROGRAM...");
            ApplicationContext context = new ApplicationContext();
            repository = new DbTodoTaskRepository(context);
        }
    }
}