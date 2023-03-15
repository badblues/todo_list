namespace Domain
{
    public class TodoTask
    {
        public TodoTask(int userId, string title, string details)
        {
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

        public override String ToString()
        {
            String str = $"id={Id}, " +
                $"userid={UserId}, " +
                $"title=\"{Title}\", " +
                $"details=\"{Details}\", " +
                $"creation-date={CreationDate}, " +
                $"edit-date={EditDate}";
            return str;
        }
    }
}

