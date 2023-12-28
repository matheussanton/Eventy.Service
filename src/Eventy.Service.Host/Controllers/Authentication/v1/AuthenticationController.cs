using Eventy.Service.Domain.Authentication.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eventy.Service.Host.Controllers.Authentication.v1
{
    [ApiController]
    [Route("api/[controller]/v1")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(
            IMediator mediator
        )
        {
            _mediator = mediator;
        }


        [HttpPost("authenticate")]
        public async Task<IActionResult> Post([FromBody] AuthenticateRequest command)
        {
            var res = await _mediator.Send(command);

            return Ok(res);
        }
    }
}
