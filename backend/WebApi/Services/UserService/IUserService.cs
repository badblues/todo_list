namespace TodoList.WebApi.Services;

public interface IUserService
{
    public string? GetUserEmail();

    public Guid? GetUserId();
}
