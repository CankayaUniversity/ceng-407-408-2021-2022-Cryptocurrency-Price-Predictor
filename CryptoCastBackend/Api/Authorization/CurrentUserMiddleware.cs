using Application.Api;
using Shared.Entities.Common;
using Shared.Extentions;

namespace Authorization.Authorization
{
    public class CurrentUserMiddleware
    {
        private readonly RequestDelegate _next;

        public CurrentUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {

            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            var claims = context.User.Claims;
            var userId = claims != null ? claims.FirstOrDefault(c => c.Type == "UserId")?.Value : string.Empty;
            var roleId = claims != null ? claims.FirstOrDefault(c => c.Type == "RoleId")?.Value : string.Empty;
            var userName = claims != null ? claims.FirstOrDefault(c => c.Type == "UserName")?.Value : string.Empty;
            var languageCode = claims != null ? claims.FirstOrDefault(c => c.Type == "LanguageCode")?.Value : string.Empty;
            var expiresDate = claims != null ? claims.FirstOrDefault(c => c.Type == "Expires")?.Value : string.Empty;
            string ip = context.Request?.HttpContext?.Connection?.RemoteIpAddress?.ToString();

            var currentUser = new CurrentUserEntity()
            {
                Id = userId.ToLong(),
                RoleId = roleId.ToInt(),
                UserName = userName,
                LanguageCode = languageCode,
                Ip = ip,
                ExpiresDate = expiresDate,
                Token = token
            };

            ProjectConfiguration.CurrentUser = currentUser;
            ProjectConfiguration.HttpContext = context;
            await _next(context);
        }
    }
}
