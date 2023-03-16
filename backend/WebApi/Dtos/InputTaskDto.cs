using System.ComponentModel.DataAnnotations;

namespace TodoList.WebApi.Dtos
{
    public class InputTaskDto
    {
        [Required]
        public bool Completed { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Details { get; set; }
    }
}
