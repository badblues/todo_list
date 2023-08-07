using TodoList.Domain;
using TodoList.WebApi.Dtos;

namespace TodoList.WebApi.Extensions;
public static class Extensions
{
    public static TodoTaskDto AsDto(this TodoTask task)
    {
        return new TodoTaskDto
        {
            Id = task.Id,
            Completed = task.Completed,
            Title = task.Title,
            Details = task.Details,
            CreationDate = task.CreationDate,
            EditDate = task.EditDate
        };
    }
    public static UserDto AsDto(this User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Email = user.Email
        };
    }
}
