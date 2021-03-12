using AuthTutorial.Auth.Api.Models;
using AuthTutorial.Auth.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthTutorial.Auth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IOptions<AuthOptions> authOptions;

        public AuthController(IOptions<AuthOptions> authOptions)
        {
            this.authOptions = authOptions;
        }

        //Вместо базы данных
        private List<Account> Accounts => new List<Account>
        {
            new Account()
            {
                Id = Guid.Parse("5fe16eac-997c-408f-8012-e078492c4f1c"),
                Email = "user@email.com",
                Password = "user",
                Roles = new Role[] { Role.User}
            },
            new Account()
            {
                Id = Guid.Parse("c68a6381-9f32-49f4-9430-a6baae4fe2c3"),
                Email = "user2@email.com",
                Password = "user2",
                Roles = new Role[] { Role.User}
            },
            new Account()
            {
                Id = Guid.Parse("b6e1955f-af39-41fa-9697-c12cb84746c8"),
                Email = "admin@email.com",
                Password = "admin",
                Roles = new Role[] { Role.Admin}
            }
        };

        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] Login request)
        {
            var user = AuthenticateUser(request.Email, request.Password);

            if(user != null)
            {
                var token = GenerateJWT(user);
                return Ok(new
                {
                    access_token = token
                }); 
            }

            return Unauthorized();

        }

        private Account AuthenticateUser(string email, string password)
        {
            return Accounts.SingleOrDefault(u => u.Email == email && u.Password == password);
        }


        private string GenerateJWT(Account user)
        {
            var authParams = authOptions.Value;

            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            };

            foreach(var role in user.Roles)
            {
                claims.Add(new Claim("role", role.ToString())) ;
            }

            var token = new JwtSecurityToken(authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
