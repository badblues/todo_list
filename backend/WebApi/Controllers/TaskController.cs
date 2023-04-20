using TodoList.Domain;
using Microsoft.AspNetCore.Mvc;
using TodoList.Persistence.Interfaces;
using TodoList.WebApi.Dtos;
using TodoList.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using TodoList.WebApi.Services.UserService;

//TODO: CRUD in context of different users

namespace TodoList.WebApi.Controllers
{
    [ApiController]
    [Route("tasks")]
    [Authorize]
    public class TodoTaskContorller : ControllerBase
    {
        private readonly ITodoTaskRepository repository;
        private readonly IUserService userService;

        public TodoTaskContorller(ITodoTaskRepository repository, IUserService userService)
        {
            this.repository = repository;
            this.userService = userService;
        }

        [HttpGet]
        public IEnumerable<TodoTaskDto> GetTodoTasks()
        {
            var tasks = repository.GetTodoTasks()
                .Where(task => task.UserId == userService.GetUserId())
                .Select( task => task.AsDto());
            return tasks;
        }

        [HttpGet("{id}")]
        public ActionResult<TodoTaskDto> GetTodoTask(Guid id)
        {
            var task = repository.GetTodoTask(id);
            if (task is null || task.UserId != userService.GetUserId())
                return NotFound();
            return task.AsDto();
        }

        [HttpPost]
        public ActionResult<TodoTaskDto> CreateTodoTask(InputTodoTaskDto taskDto)
        {
            TodoTask task = new()
            {
                Id = Guid.NewGuid(),
                UserId = userService.GetUserId(),
                Title = taskDto.Title,
                Details = taskDto.Details,
                Completed = taskDto.Completed,
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
            if (existingTask is null || existingTask.UserId != userService.GetUserId())
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
            if (existingTask is null || existingTask.UserId != userService.GetUserId())
            {
                return NotFound();
            }
            repository.DeleteTodoTask(id);
            return NoContent();
        }
    }
}
