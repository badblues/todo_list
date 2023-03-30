using TodoList.WebApi.Dtos;

namespace WebApi.Extensions
{
    public static class Extensions
    {
        public static TodoTaskDto AsDto(this TodoList.Domain.TodoTask task)
        {
            return new TodoTaskDto
            {
                Id = task.Id,
                UserId = task.UserId,
                Title = task.Title,
                Details = task.Details,
                CreationDate = task.CreationDate,
                EditDate = task.EditDate
            };
        }
    }
}
