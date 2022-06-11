using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Api;
using Shared.Entities.Common;
using Shared.Extentions;
using Microsoft.IdentityModel.Tokens;

namespace Authorization.Authorization
{
    public static class JwtToken
    {
        public static UserTokenEntity GetToken(CurrentUserEntity user)
        {
            var iss = ProjectConfiguration.Configuration.GetSection("Audience").GetSection("Iss").Value;
            var aud = ProjectConfiguration.Configuration.GetSection("Audience").GetSection("Aud").Value;

            var xmlKey=File.ReadAllText(@"Authorization//rsa-private-key.xml");

            SecurityKey key = JwtKeyHelper.BuildRsaSigningKey(xmlKey);

            DateTime expires = DateTime.UtcNow.AddHours(12);
            var claims = new[]
            {
                    new Claim("UserId", user.Id.ToStr()),
                    new Claim("RoleId", user.RoleId.ToStr()),
                    new Claim("UserName", user.UserName.ToStr()),
                    new Claim("LanguageCode", user.LanguageCode.ToStr()),
                    new Claim("Expires",expires.ToStr())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = iss,
                Audience = aud,
                Subject = new ClaimsIdentity(claims),
                Expires = expires,// 12 saat geçerli olacak
                NotBefore = DateTime.Now,
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(securityToken);

            return new UserTokenEntity(){Token = jwtToken,ExpiresDate = expires };
        }
    }
}
