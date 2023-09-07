using System.Security.Claims;

namespace TodoList.WebApi.Services;

public class UserService : IUserService
{
    public IHttpContextAccessor _httpContextAccessor;

    public UserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? GetUserId()
    {
        if ((_httpContextAccessor is null) || (_httpContextAccessor.HttpContext is null))
            throw new NullReferenceException();

        string? id = _httpContextAccessor.HttpContext.User.FindFirstValue("id");
        if (id is null)
            return null;
        return Guid.Parse(id);
    }
}
