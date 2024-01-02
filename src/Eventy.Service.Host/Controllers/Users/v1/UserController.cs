using Eventy.Service.Domain.User.Commands;
using Eventy.Service.Domain.User.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eventy.Service.Host.Controllers.Users.v1
{
    [ApiController]
    [Route("api/[controller]/v1")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserRepository _userRepository;

        public UserController(
            IMediator mediator,
            IUserRepository userRepository
        )
        {
            _mediator = mediator;
            _userRepository = userRepository;
        }


        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateUserCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return Ok(users);
        }
    }
}
