using Shared.Entities.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Application.Api
{
    public static class ProjectConfiguration
    {
        public static IConfiguration Configuration;
        public static CurrentUserEntity CurrentUser;
        public static HttpContext HttpContext;
    }
}
