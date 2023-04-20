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
            var result = string.Empty;
            if (httpContextAccessor != null)
            {
                result = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            }
            return result;
        }

        public Guid GetUserId()
        {
            if (httpContextAccessor != null)
            {
                string id = httpContextAccessor.HttpContext.User.FindFirstValue("userId");
                return Guid.Parse(id);
            }
            return Guid.Empty;
        }
    }
}
