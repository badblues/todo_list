namespace Domain
{
    public class TodoTask
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public bool Completed { get; set; }
        public string? Title { get; set; }
        public string? Details { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? EditDate { get; set; }
    }
}

