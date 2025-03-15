using IdentityServer.BusinessLogic.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using IdentityServer.Entities.ApplicationUser;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using IdentityServer.BusinessLogic.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;



namespace IdentityServer.BusinessLogic.Services
{
    public class TokenProvider : ITokenProvider
    {
       
        private readonly JwtOptions  _jwtOptions;

        public TokenProvider(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public string Create(IdentityServer.Entities.Dtos.UserDto user)
        {
            string secretKey = _jwtOptions.Secret;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credintials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim (JwtRegisteredClaimNames.UniqueName, user.Name),

                ]),
                Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationInMinutes),
                SigningCredentials = credintials,
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audience,
            };

            var handler = new JsonWebTokenHandler();

            string token = handler.CreateToken(tokenDescriptor);

            return token;
        }
    }
}
