using Eventy.Service.Domain.Events.Commands;
using Eventy.Service.Domain.Events.Queries.Requests;
using Eventy.Service.Domain.User.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eventy.Service.Host.Controllers.Events.v1
{
    [ApiController]
    [Route("api/[controller]/v1")]
    [Authorize]
    public class EventsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventsController(
            IMediator mediator
        )
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateEventCommand command)
        {
            var userIdClaim = User.GetUserId();
            if(userIdClaim == null) return Unauthorized();

            command.UserId = Guid.Parse(userIdClaim);

            await _mediator.Send(command);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateEventCommand command)
        {
            var userIdClaim = User.GetUserId();
            if(userIdClaim == null) return Unauthorized();

            command.UserId = Guid.Parse(userIdClaim);
        
            await _mediator.Send(command);

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(
            [FromRoute] Guid id)
        {
            var request = new GetEventRequest(id);

            var result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var userIdClaim = User.GetUserId();
            if(userIdClaim == null) return Unauthorized();

            var request = new GetEventsRequest(Guid.Parse(userIdClaim));

            var result = await _mediator.Send(request);

            return Ok(result);
        }
    }
}
