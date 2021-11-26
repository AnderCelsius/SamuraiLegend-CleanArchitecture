using Microsoft.AspNetCore.Http;
using SamuraiLegend.Application.Interfaces;
using System.Security.Claims;

namespace SamuraiLegend.WebApi.Services
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("uid");
        }

        public string UserId { get; }
    }
}
