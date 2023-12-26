using Eventy.Service.Domain.Entities;
using Eventy.Service.Domain.Events.Interfaces;
using Eventy.Service.Domain.Events.Queries.Requests;
using MediatR;

namespace Eventy.Service.Domain.Events.Queries
{
    public class EventsQueryHandler : IRequestHandler<GetEventRequest, EventEntityDomain?>,
                                      IRequestHandler<GetEventsRequest, List<EventEntityDomain>>
    {

        private readonly IEventRepository _eventRepository;

        public EventsQueryHandler(
            IEventRepository eventRepository
        )
        {
            _eventRepository = eventRepository;
        }
        
        public async Task<EventEntityDomain?> Handle(GetEventRequest request, CancellationToken cancellationToken)
        {
            return await _eventRepository.GetByIdAsync(request.Id);
        }

        public async Task<List<EventEntityDomain>> Handle(GetEventsRequest request, CancellationToken cancellationToken)
        {
            return await _eventRepository.GetAllAsync(request.Id);
        }
    }
}
