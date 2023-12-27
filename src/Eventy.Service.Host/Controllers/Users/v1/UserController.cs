using Eventy.Service.Domain.User.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eventy.Service.Host.Controllers.Users.v1
{
    [ApiController]
    [Route("api/[controller]/v1")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(
            IMediator mediator
        )
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }
    }
}
