using Eventy.Service.Domain.Responses;
using Eventy.Service.Domain.Responses.Enums;
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
        public async Task<IActionResult> PostAsync([FromBody] CreateUserCommand command,        
                                                   [FromServices] Response response)
        {
            await _mediator.Send(command);

            if(response.Status == ResponseStatus.Fail)
            {
                return StatusCode((int)response.StatusCode, response.Notifications);
            }
            
            return Ok(response.Notifications);
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
