using TodoList.Domain;
using Microsoft.AspNetCore.Mvc;
using TodoList.Persistence.Interfaces;
using TodoList.WebApi.Dtos;
using TodoList.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using TodoList.WebApi.Services;

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
            _repository = repository;
            _userService = userService;
        }

        [HttpGet]
        public IEnumerable<TodoTaskDto> GetTodoTasks()
        {
            var tasks = _repository.GetAll()
                .Where(task => task.UserId == _userService.GetUserId())
                .Select( task => task.AsDto());
            return tasks;
        }

        [HttpGet("{id}")]
        public ActionResult<TodoTaskDto> GetTodoTask(Guid id)
        {
            var task = _repository.Get(id);
            if (task is null || task.UserId != _userService.GetUserId())
                return NotFound();
            return task.AsDto();
        }

        [HttpPost]
        public ActionResult<TodoTaskDto> CreateTodoTask(InputTodoTaskDto taskDto)
        {
            var userId = _userService.GetUserId();
            if (userId is null)
                return Unauthorized();
            TodoTask task = new()
            {
                Id = Guid.NewGuid(),
                UserId = (Guid)userId,
                Title = taskDto.Title,
                Details = taskDto.Details,
                Completed = taskDto.Completed,
                CreationDate = DateTime.UtcNow,
                EditDate = null
            };
            _repository.Create(task);
            return CreatedAtAction(nameof(GetTodoTask), new {id = task.Id}, task.AsDto());
        }

        [HttpPut("{id}")]
        public ActionResult UpdateTodoTask(Guid id, InputTodoTaskDto taskDto)
        {
            var existingTask = _repository.Get(id);
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
            _repository.Update(updatedTask);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTask(Guid id)
        {
            var existingTask = _repository.Get(id);
            if (existingTask is null || existingTask.UserId != _userService.GetUserId())
            {
                return NotFound();
            }
            _repository.Delete(id);
            return NoContent();
        }
    }
}
