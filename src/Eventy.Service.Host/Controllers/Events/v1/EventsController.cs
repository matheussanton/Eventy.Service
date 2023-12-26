using Eventy.Service.Domain.Events.Commands;
using Eventy.Service.Domain.Events.Queries;
using Eventy.Service.Domain.Events.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eventy.Service.Host.Controllers.Events.v1
{
    [ApiController]
    [Route("api/[controller]/v1")]
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
            await _mediator.Send(command);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateEventCommand command)
        {
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

        [HttpGet("{userId}/all")]
        public async Task<IActionResult> GetAllAsync(
            [FromRoute] Guid userId)
        {
            var request = new GetEventsRequest(userId);

            var result = await _mediator.Send(request);

            return Ok(result);
        }
    }
}
