using TodoList.Domain;
using Microsoft.AspNetCore.Mvc;
using TodoList.Persistence.Interfaces;
using TodoList.WebApi.Dtos;
using TodoList.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using TodoList.WebApi.Services.UserService;

namespace TodoList.WebApi.Controllers
{
    [ApiController]
    [Route("tasks")]
    [Authorize]
    public class TodoTaskContorller : ControllerBase
    {
        private readonly ITodoTaskRepository _repository;
        private readonly IUserService _userService;

        public TodoTaskContorller(ITodoTaskRepository repository, IUserService userService)
        {
            this._repository = repository;
            this._userService = userService;
        }

        [HttpGet]
        public IEnumerable<TodoTaskDto> GetTodoTasks()
        {
            var tasks = _repository.GetTodoTasks()
                .Where(task => task.UserId == _userService.GetUserId())
                .Select( task => task.AsDto());
            return tasks;
        }

        [HttpGet("{id}")]
        public ActionResult<TodoTaskDto> GetTodoTask(Guid id)
        {
            var task = _repository.GetTodoTask(id);
            if (task is null || task.UserId != _userService.GetUserId())
                return NotFound();
            return task.AsDto();
        }

        [HttpPost]
        public ActionResult<TodoTaskDto> CreateTodoTask(InputTodoTaskDto taskDto)
        {
            TodoTask task = new()
            {
                Id = Guid.NewGuid(),
                UserId = _userService.GetUserId(),
                Title = taskDto.Title,
                Details = taskDto.Details,
                Completed = taskDto.Completed,
                CreationDate = DateTime.UtcNow,
                EditDate = null
            };
            _repository.CreateTodoTask(task);
            return CreatedAtAction(nameof(GetTodoTask), new {id = task.Id}, task.AsDto());
        }

        [HttpPut("{id}")]
        public ActionResult UpdateTodoTask(Guid id, InputTodoTaskDto taskDto)
        {
            var existingTask = _repository.GetTodoTask(id);
            if (existingTask is null || existingTask.UserId != _userService.GetUserId())
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
            _repository.UpdateTodoTask(updatedTask);
            return NoContent();
        }

        [HttpDelete]
        public ActionResult DeleteTask(Guid id)
        {
            var existingTask = _repository.GetTodoTask(id);
            if (existingTask is null || existingTask.UserId != _userService.GetUserId())
            {
                return NotFound();
            }
            _repository.DeleteTodoTask(id);
            return NoContent();
        }
    }
}
