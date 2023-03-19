using TodoList.WebApi.Dtos;

namespace WebApi.Extensions
{
    public static class Extensions
    {
        public static TaskDto AsDto(this TodoList.Domain.Task task)
        {
            return new TaskDto
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
