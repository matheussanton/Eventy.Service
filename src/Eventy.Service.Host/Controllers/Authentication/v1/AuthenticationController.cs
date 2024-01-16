using Eventy.Service.Domain.Authentication.Commands;
using Eventy.Service.Domain.Responses;
using Eventy.Service.Domain.Responses.Enums;
using Eventy.Service.Domain.User.Models;
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
        public async Task<IActionResult> Post([FromBody] AuthenticateRequest command,
                                                [FromServices] Response<UserDTO> response)
        {
            await _mediator.Send(command);

            if(response.Status == ResponseStatus.Fail)
            {
                return StatusCode((int)response.StatusCode, response.Notifications);
            }
            
            return Ok(response.Value);
        }
    }
}
