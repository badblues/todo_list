using TodoList.WebApi.Dtos;

namespace TodoList.WebApi.Extensions
{
    public static class Extensions
    {
        public static TodoTaskDto AsDto(this TodoList.Domain.TodoTask task)
        {
            return new TodoTaskDto
            {
                Id = task.Id,
                UserId = task.UserId,
                Completed = task.Completed,
                Title = task.Title,
                Details = task.Details,
                CreationDate = task.CreationDate,
                EditDate = task.EditDate
            };
        }
        public static UserDto AsDto(this TodoList.Domain.User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Email = user.Email
            };
        }
    }
}
