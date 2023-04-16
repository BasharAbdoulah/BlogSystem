using BlogSystem.AppModel;
using BlogSystem.DBModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogSystem.Controllers
{

    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly BlogDataContext context;

        public LoginController(IConfiguration configuration, BlogDataContext context)
        {
            
            this.configuration = configuration;
            this.context = context;
        }



        [HttpPost]
        [Route("PostLoginDetails")]
        public async Task<IActionResult> PostLoginDetails(UserModel _userData)
        {
            if(_userData!=null)
            {
                var resultLoginCheck =  context.Users
                    .Where(e => e.Email == _userData.Email && e.Password == _userData.Password)
                    .FirstOrDefault();

                if(resultLoginCheck==null)
                {
                    return BadRequest("Invalid Credentials");
                }
                else
                {
                    _userData.UserMessage = "Login Success";
                    _userData.Admin = resultLoginCheck.Admin;
                    

                    var claims = new List<Claim> {
                        new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", _userData.ID.ToString()),
                        new Claim("Admin", resultLoginCheck.Admin.ToString()),
                        new Claim("UserName", _userData.FullName),
                        new Claim("Email", _userData.Email),
                        new Claim(ClaimTypes.Role, "User")

                    };

                    if (resultLoginCheck.Admin == true)
                    {

                        claims[7] = new Claim(ClaimTypes.Role, "Admin");
                    }


                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        configuration["Jwt:Issuer"],
                        configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);


                    _userData.AccessToken = new JwtSecurityTokenHandler().WriteToken(token);

                    return Ok(_userData);
                }
            }
            else
            {
                return BadRequest("No Data Posted");
            }
        }


       
    }
}
