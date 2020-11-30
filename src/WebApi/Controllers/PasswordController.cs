using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        [HttpGet("checker")]
        public async Task<IActionResult> StrengthChecker([FromQuery] string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return BadRequest("Password cannot be empty or null");
            }

            return Ok(false);
        }

        
    }
}
