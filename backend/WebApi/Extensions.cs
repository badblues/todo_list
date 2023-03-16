using TodoList.Domain;
using TodoList.WebApi.Dtos;

namespace TodoList.WebApi
{
    public static class Extensions
    {
        public static TaskDto AsDto(this Domain.Task task)
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
