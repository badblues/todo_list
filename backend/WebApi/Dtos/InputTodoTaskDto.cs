using System.ComponentModel.DataAnnotations;

namespace TodoList.WebApi.Dtos;
public class InputTodoTaskDto
{
    [Required]
    public bool Completed { get; set; }
    [Required]
    public string Title { get; set; } = String.Empty;

    public string Details { get; set; } = String.Empty;
}
