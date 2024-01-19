using Eventy.Service.Domain.Enums;
using Eventy.Service.Domain.Events.Commands;
using Eventy.Service.Domain.Events.Models;
using Eventy.Service.Domain.Events.Queries.Requests;
using Eventy.Service.Domain.Responses;
using Eventy.Service.Domain.Responses.Enums;
using Eventy.Service.Domain.User.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Eventy.Service.Domain.Extensions;

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
        [ProducesResponseType(typeof(SelectEvent), 200)]
        public async Task<IActionResult> GetAsync(
            [FromRoute] Guid id)
        {
            var request = new GetEventRequest(id);

            var result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(List<SelectEvent>), 200)]
        public async Task<IActionResult> GetAllAsync()
        {
            var userIdClaim = User.GetUserId();
            if(userIdClaim == null) return Unauthorized();

            var request = new GetEventsRequest(Guid.Parse(userIdClaim));

            var result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpGet("pending")]
        [ProducesResponseType(typeof(List<SelectEvent>), 200)]
        public async Task<IActionResult> GetPendingAsync()
        {
            var userIdClaim = User.GetUserId();
            if(userIdClaim == null) return Unauthorized();

            var request = new GetPendingEventsRequest(Guid.Parse(userIdClaim));

            var result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id,
                                                     [FromServices] Response response)
        {
            var userIdClaim = User.GetUserId();
            if(userIdClaim == null) return Unauthorized();

            var request = new DeleteEventCommand(id, Guid.Parse(userIdClaim));

            await _mediator.Send(request);

            if(response.Status == ResponseStatus.Fail)
            {
                return StatusCode((int)response.StatusCode, response.Notifications);
            }
            
            return Ok(response);
        }


        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(SelectEvent), 200)]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id,
                                                  [FromQuery] EStatus status,
                                                  [FromServices] Response response)
        {
            var userId = User.GetUserId();
            if(userId == null) return Unauthorized();

            var request = new UpdateEventStatusCommand(id, userId.ToGuid(), status);

            await _mediator.Send(request);

            if(response.Status == ResponseStatus.Fail)
            {
                return StatusCode((int)response.StatusCode, response.Notifications);
            }
            
            return Ok(response);
        }
    }
}
