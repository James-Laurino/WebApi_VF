using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.IdentityModel.Tokens;

using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WebApi.Data;
using WebApi;

namespace WebApiJwtToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JWTTokenControler : ControllerBase
    {
        public IConfiguration _configuration;
        public readonly DataContext _context;

        public JWTTokenControler(IConfiguration configuration, DataContext context)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Post (User user)
        {
            if(user != null && user.UserName != null && user.Password != null)
            {
                var userData = GetUser(user.UserName, user.Password);
                var jwt = _configuration.GetSection("Jwt").Get<Jwt>();
                if(userData != null)
                {
                    var claims = new []
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Id", user.UserId.ToString()),
                        new Claim("UserName", user.UserName),
                        new Claim("Password", user.Password)

                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.key));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token= new JwtSecurityToken(
                        jwt.Issuer,
                        jwt.Audience,
                        claims,
                        expires: DateTime.Now.AddMinutes(1),
                        signingCredentials: signIn
                    );
                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else 
                {
                    return BadRequest("Invalid Crediential");
                }
            }
            else  
            {
                return BadRequest("Invalid Crediential");
            }

        }

        private User GetUser(string username, string password)
        {
            List<User> listUser =  _context.User.ToList();

            foreach (var item in listUser)
            {
                if(item.UserName == username && item.Password == password)
                {
                    return item;
                }
            }

            return null;
        }
    }
}