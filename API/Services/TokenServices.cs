using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenServices : ITokenServices
    {
        private readonly SymmetricSecurityKey _key;
        public TokenServices(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        public string CreateToken(AppUsers user)
        {
            var claim = new List<Claim>{
                 new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokendescriptor = new SecurityTokenDescriptor{
                Subject  = new ClaimsIdentity(claim),
                Expires = DateTime.Now.AddMinutes(120),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tkn = tokenHandler.CreateToken(tokendescriptor);

            return tokenHandler.WriteToken(tkn);
        }
    }
}