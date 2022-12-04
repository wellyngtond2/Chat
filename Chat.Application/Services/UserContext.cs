using Chat.DataContracts.Context;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Chat.Application.Services
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public UserDataContext GetUserContext()
        {

            var id = Convert.ToInt32(_httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "Id")?.Value);
            var email = _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Email)?.Value;
            var name = _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Name)?.Value;
            return new UserDataContext(id, name, email);
        }
    }
}
