namespace TodoList.Domain
{
    public record TodoTask
    {
        public Guid Id { get; init; }
        public Guid UserId { get; init; }
        public bool Completed { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? EditDate { get; set; }
    }
}
