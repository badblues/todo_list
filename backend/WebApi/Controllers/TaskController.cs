using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Domain;
using TodoList.Persistence.Interfaces;
using TodoList.WebApi.Dtos;
using TodoList.WebApi.Extensions;
using TodoList.WebApi.Services;

namespace TodoList.WebApi.Controllers;

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
    public Response<IEnumerable<TodoTaskDto>> GetTodoTasks()
    {
        Response<IEnumerable<TodoTaskDto>> response = new();
        var tasks = _repository.GetAll()
            .Where(task => task.UserId == _userService.GetUserId())
            .Select(task => task.AsDto());
        response.Data = tasks;
        return response;
    }

    [HttpGet("{id}")]
    public ActionResult<Response<TodoTaskDto>> GetTodoTask(Guid id)
    {
        Response<TodoTaskDto> response = new();
        var task = _repository.Get(id);
        if (task is null || task.UserId != _userService.GetUserId())
        {
            response.Error = "Task not found";
            return NotFound(response);
        }
        response.Data = task.AsDto();
        return response;
    }

    [HttpPost]
    public ActionResult<Response<TodoTaskDto>> CreateTodoTask(InputTodoTaskDto taskDto)
    {
        Response<TodoTaskDto> response = new();
        var userId = _userService.GetUserId();
        if (userId is null)
        {
            return Unauthorized(response);
        }
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
        var createdTask = _repository.Get(task.Id);
        if (createdTask is null)
        {
            response.Error = "Server error";
            return StatusCode(500, response);
        }
        return response;
    }

    [HttpPut("{id}")]
    public ActionResult<Response<TodoTaskDto>> UpdateTodoTask(Guid id, InputTodoTaskDto taskDto)
    {
        Response<TodoTaskDto> response = new();
        var existingTask = _repository.Get(id);
        if (existingTask is null || existingTask.UserId != _userService.GetUserId())
        {
            response.Error = "Task not found";
            return NotFound(response);
        }
        TodoTask updatedTask = existingTask with
        {
            Title = taskDto.Title,
            Details = taskDto.Details,
            Completed = taskDto.Completed,
            EditDate = DateTime.UtcNow
        };
        _repository.Update(updatedTask);
        var savedTask = _repository.Get(updatedTask.Id);
        if (savedTask is null || (savedTask != updatedTask))
        {
            response.Error = "Server error";
            return StatusCode(500, response);
        }
        return response;
    }

    [HttpDelete("{id}")]
    public ActionResult<Response<string>> DeleteTask(Guid id)
    {
        Response<string> response = new();
        var existingTask = _repository.Get(id);
        if (existingTask is null || existingTask.UserId != _userService.GetUserId())
        {
            response.Error = "Task not found";
            return NotFound();
        }
        _repository.Delete(id);
        var deletedTask = _repository.Get(id);
        if (deletedTask is not null)
        {
            response.Error = "Server error";
            return StatusCode(500, response);
        }
        response.Data = "Success";
        return response;
    }
}
