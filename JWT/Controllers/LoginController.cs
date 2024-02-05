using JWT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.CodeDom.Compiler;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;

        public LoginController(IConfiguration configuration)
        {
            _config=configuration;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var currentUser = GetCurrentUser();
            return Ok($"Hola soy {currentUser.FirtsName}, y mi rol asignado es {currentUser.Rol}");
        }


        [HttpPost]
        public IActionResult Login(UserDTO UserLog)
        {
            var user = Auth(UserLog);
            if (user != null)
            {
                //creamo el token
                var token = Generate(user);

                return Ok(token);
            }
            return NotFound("Usuario no encontrado");
        }


        //  Mostrar los datos del Usuario authenticado

        private UserModel Auth(UserDTO User)
        {

            var userLog = Constants.Constants.user.FirstOrDefault(d => d.UserName == User.UserName && d.Pass == User.Pass);
            if (userLog != null)
            {
                return userLog;
            }
            return null;
        }


        //   Obtener los datos del Usuario Authenticado

        private UserModel GetCurrentUser()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if (identity!.Claims.Count() != 0)
                {
                    return new UserModel
                    {
                        UserName = identity.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)!.Value,
                        EmailAddress = identity.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Email)!.Value,
                        FirtsName = identity.Claims.FirstOrDefault(e => e.Type == ClaimTypes.GivenName)!.Value,
                        LastName = identity.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Surname)!.Value,
                        Rol = identity.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)!.Value
                    };
                }  
            }  
            return null;
        }


        //  Generamos Los Claims Y los Tokens

        private string Generate(UserModel user)
        {
            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:key"]));
            var Credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

            //Crear los claims
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Email, user.EmailAddress),
                new Claim(ClaimTypes.GivenName, user.FirtsName),
                new Claim(ClaimTypes.Surname,user.LastName),
                new Claim(ClaimTypes.Role,user.Rol)
            };
            //Crear el Token
            var token = new JwtSecurityToken
            (
                _config["JWT:Issuer"],
                _config["JWT:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: Credentials       
              );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
