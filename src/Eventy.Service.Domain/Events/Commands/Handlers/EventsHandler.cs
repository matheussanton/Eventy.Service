using Eventy.Service.Domain.Events.Interfaces;
using MediatR;

namespace Eventy.Service.Domain.Events.Commands.Handlers
{
    public class EventsHandler : IRequestHandler<CreateEventCommand>,
                                    IRequestHandler<UpdateEventCommand>
    {
        private readonly IEventRepository _eventRepository;

        public EventsHandler(
            IEventRepository eventRepository
        )
        {
            _eventRepository = eventRepository;
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

            await _eventRepository.UpdateAsync(entity, record);

            return;
        }
    }
}
