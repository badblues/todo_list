using TodoList.Domain;
using Microsoft.AspNetCore.Mvc;
using TodoList.Persistence.Interfaces;
using TodoList.WebApi.Dtos;
using WebApi.Extensions;

namespace TodoList.WebApi.Controllers
{
    [ApiController]
    [Route("tasks")]
    public class TodoTaskContorller : ControllerBase
    {
        private readonly ITodoTaskRepository repository;

        public TodoTaskContorller(ITodoTaskRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IEnumerable<TodoTaskDto> GetTodoTasks()
        {
            var tasks = repository.GetTodoTasks().Select( task => task.AsDto());
            return tasks;
        }

        [HttpGet("{id}")]
        public ActionResult<TodoTaskDto> GetTodoTask(Guid id)
        {
            var task = repository.GetTodoTask(id);
            if (task is null)
                return NotFound();
            return task.AsDto();
        }

        [HttpPost]
        public ActionResult<TodoTaskDto> CreateTodoTask(InputTodoTaskDto taskDto)
        {
            TodoTask task = new()
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Title = taskDto.Title,
                Details = taskDto.Details,
                Completed = false,
                CreationDate = DateTime.UtcNow,
                EditDate = null
            };
            repository.CreateTodoTask(task);
            return CreatedAtAction(nameof(GetTodoTask), new {id = task.Id}, task.AsDto());
        }

        [HttpPut("{id}")]
        public ActionResult UpdateTodoTask(Guid id, InputTodoTaskDto taskDto)
        {
            var existingTask = repository.GetTodoTask(id);
            if (existingTask is null)
            {
                return NotFound();
            }
            TodoTask updatedTask = existingTask with
            {
                Title = taskDto.Title,
                Details = taskDto.Details,
                Completed = taskDto.Completed,
                EditDate = DateTime.UtcNow
            };
            repository.UpdateTodoTask(updatedTask);
            return NoContent();
        }

        [HttpDelete]
        public ActionResult DeleteTask(Guid id)
        {
            var existingTask = repository.GetTodoTask(id);
            if (existingTask is null)
            {
                return NotFound();
            }
            repository.DeleteTodoTask(id);
            return NoContent();
        }
    }
}
