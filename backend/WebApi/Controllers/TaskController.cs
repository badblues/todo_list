using TodoList.Domain;
using Microsoft.AspNetCore.Mvc;
using TodoList.Persistence.Interfaces;
using TodoList.WebApi.Dtos;
using System.ComponentModel.DataAnnotations;

namespace TodoList.WebApi.Controllers
{
    [ApiController]
    [Route("tasks")]
    public class TaskContorller : ControllerBase
    {
        private readonly ITaskRepository repository;

        public TaskContorller(ITaskRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IEnumerable<TaskDto> GetTodoTasks()
        {
            var tasks = repository.GetTasks().Select( task => task.AsDto());
            return tasks;
        }

        [HttpGet("{id}")]
        public ActionResult<TaskDto> GetTodoTask(Guid id)
        {
            var task = repository.GetTask(id);
            if (task is null)
            {
                return NotFound();
            }
            return task.AsDto();
        }

        [HttpPost]
        public ActionResult<TaskDto> CreateTodoTask(InputTaskDto taskDto)
        {
            Domain.Task task = new()
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Title = taskDto.Title,
                Details = taskDto.Details,
                Completed = false,
                CreationDate = DateTime.UtcNow,
                EditDate = null
            };
            repository.CreateTask(task);
            return CreatedAtAction(nameof(GetTodoTask), new {id = task.Id}, task.AsDto());
        }

        [HttpPut("{id}")]
        public ActionResult UpdateTodoTask(Guid id, InputTaskDto taskDto)
        {
            var existingTask = repository.GetTask(id);
            if (existingTask is null)
            {
                return NotFound();
            }
            Domain.Task updatedTask = existingTask with
            {
                Title = taskDto.Title,
                Details = taskDto.Details,
                Completed = taskDto.Completed,
                EditDate = DateTime.UtcNow
            };
            repository.UpdateTask(updatedTask);
            return NoContent();
        }

        [HttpDelete]
        public ActionResult DeleteTask(Guid id)
        {
            var existingTask = repository.GetTask(id);
            if (existingTask is null)
            {
                return NotFound();
            }
            repository.DeleteTask(id);
            return NoContent();
        }
    }
}
