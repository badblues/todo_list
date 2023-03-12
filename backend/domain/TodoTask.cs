namespace Domain
{
    public class TodoTask
    {
        public TodoTask(int id, int userId, string title, string details)
        {
            Id = id;
            UserId = userId;
            Title = title;
            Details = details;
            CreationDate = DateTime.Now;
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public bool Completed { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? EditDate { get; set; }
    }
}

