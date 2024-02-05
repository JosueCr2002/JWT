using JWT.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        [HttpGet]
        //[Authorize(Roles =("Administrador"))]

        public IActionResult ListEmployee()
        {
            if (User.IsInRole("Administrador")) //validamos que sea administrador ya que en los claims se agregaron los roles
            {
                var emp = EmployeeConstants.Employe;
                return Ok(emp);
            }
            else
            {
                return Unauthorized("Acceso no autorizado. Se requiere el rol de Administrador.");
            }
          
        }
    }
}
