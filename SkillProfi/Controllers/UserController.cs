using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SkillProfiApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("webapi/[controller]")]
    public class UserController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("GetToken")]
        public IActionResult GetToken()
        {
            return Ok("dfds");
        }
        [HttpPost("ReNewToken")]
        public IActionResult ReNewToken() 
        {
            return Ok("newtoken");
        }
    }
}
