namespace TodoList.WebApi.Dtos
{
    public class TodoTaskDto
    {
        public Guid Id { get; init; }
        public Guid UserId { get; init; }
        public bool Completed { get; set; }
        public string Title { get; set; } = String.Empty;
        public string Details { get; set; } = String.Empty;
        public DateTime CreationDate { get; set; }
        public DateTime? EditDate { get; set; }
    }
}
