using System.Net;
using Eventy.Service.Domain.Events.Interfaces;
using Eventy.Service.Domain.Responses;
using Eventy.Service.Domain.Responses.Enums;
using MediatR;

namespace Eventy.Service.Domain.Events.Commands.Handlers
{
    public class EventsHandler : IRequestHandler<CreateEventCommand>,
                                    IRequestHandler<UpdateEventCommand>,
                                    IRequestHandler<DeleteEventCommand>
    {
        private readonly IEventRepository _eventRepository;
         private readonly Response _response;

        public EventsHandler(
            IEventRepository eventRepository,
            Response response
        )
        {
            _eventRepository = eventRepository;
            _response = response;
        }
        
        
        public async Task Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var eventEntity = request.Parse();

            await _eventRepository.CreateAsync(eventEntity);

            return;
        }

        public async Task Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var record = await _eventRepository.GetByIdAsync(request.Id);

            if (record == null)
            {
                throw new Exception("Event not found");
            }

            var entity = request.Parse(record);

            await _eventRepository.UpdateAsync(entity);

            return;
        }

        public async Task Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var record = await _eventRepository.GetByIdAsync(request.Id);

            if (record == null)
            {
                request.AddNotification("Id", "Event not found");
                _response.Send(ResponseStatus.Fail, HttpStatusCode.BadRequest, request.Notifications);
            }

            await _eventRepository.DeleteAsync(request.Id, request.UserId);

            _response.Send(ResponseStatus.Success, HttpStatusCode.OK);
        }
    }
}
