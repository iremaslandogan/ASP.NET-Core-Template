using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Token
{
    public class TokenService : ITokenService
    {
        private readonly JwtOption _jwtOptions;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenService(JwtOption jwtOptions, IHttpContextAccessor httpContextAccessor)
        {
            _jwtOptions = jwtOptions;
            _httpContextAccessor = httpContextAccessor;
        }

        public string CreateToken(int id, string phone, string name, string lastname, int role)
        {
            //Console.WriteLine(JwtOption);
            Console.WriteLine(_jwtOptions);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecurityKey));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var jwt = CreateJwtSecurityToken(id, phone, name, lastname, role, signingCredentials);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return token;
        }

        public string CreateMailToken(string phone)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecurityKey));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var jwt = new JwtSecurityToken(
                _jwtOptions.Issuer,
                _jwtOptions.Audience,
                expires: DateTime.Now.AddMinutes(15),  // Expire date of mail token is 5 minutes
                notBefore: DateTime.Now,
                claims: new List<Claim>
                {
                    new Claim("phone", phone),
                },
                signingCredentials: signingCredentials
            );
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return token;
        }

        public async Task<List<Claim>> GetUserClaims()
        {
            return await Task.Run(() =>
               new List<Claim>
               {
                     _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier),
                     _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)
               }
           );
        }

        private JwtSecurityToken CreateJwtSecurityToken(int id, string phone, string name, string lastname, int role, SigningCredentials signingCredentials)
        {
            Console.WriteLine("0000000000000000000000000000000000000");
            Console.WriteLine(Convert.ToInt32(role));
            
            var jwt = new JwtSecurityToken(
                _jwtOptions.Issuer,
                _jwtOptions.Audience,
                expires: DateTime.Now.AddMinutes(_jwtOptions.AccessTokenExpiration),
                notBefore: DateTime.Now,
                claims: new List<Claim>
                {
                    new Claim("id", id.ToString()),
                    new Claim("name", name),
                    new Claim("lastname", lastname),
                    new Claim("phone", phone),
                    //new Claim("role", role.ToString()),
                    new Claim(ClaimTypes.Role, Convert.ToInt32(role).ToString())
                },
                signingCredentials: signingCredentials

            );
            return jwt;
        }
    }
}
