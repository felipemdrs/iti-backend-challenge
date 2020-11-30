using System.Threading.Tasks;
using Domain.Commands;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        private IMediator _mediator;

        public PasswordController(IMediator mediator): base()
        {
            _mediator = mediator;
        }

        [HttpPost("validate")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> Validate([FromBody] ValidatePassword validatePassword)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isValid = await _mediator.Send(validatePassword).ConfigureAwait(false);

            return Ok(new TransferModel<bool>()
            {
                Data = isValid
            });
        }
    }
}
