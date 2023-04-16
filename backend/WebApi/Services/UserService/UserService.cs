﻿using System.Security.Claims;

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
    }
}
