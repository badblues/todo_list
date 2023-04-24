using System.Security.Claims;

namespace TodoList.WebApi.Services.UserService
{
    public class UserService : IUserService
    {
        public IHttpContextAccessor httpContextAccessor { get; }

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }


        public string GetUserEmail()
        {
            if ((httpContextAccessor is null) || (httpContextAccessor.HttpContext is null))
              throw new NullReferenceException();
                
            return httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
        }

        public Guid GetUserId()
        {
            if ((httpContextAccessor is null) || (httpContextAccessor.HttpContext is null))
              throw new NullReferenceException();
              
            string id = httpContextAccessor.HttpContext.User.FindFirstValue("userId");
            return Guid.Parse(id);
        }
    }
}
