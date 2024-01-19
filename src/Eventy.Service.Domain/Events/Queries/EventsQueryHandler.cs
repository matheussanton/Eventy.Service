using Eventy.Service.Domain.Entities;
using Eventy.Service.Domain.Events.Interfaces;
using Eventy.Service.Domain.Events.Models;
using Eventy.Service.Domain.Events.Queries.Requests;
using MediatR;

namespace Eventy.Service.Domain.Events.Queries
{
    public class EventsQueryHandler : IRequestHandler<GetEventRequest, SelectEvent?>,
                                      IRequestHandler<GetEventsRequest, List<SelectEvent>>,
                                      IRequestHandler<GetPendingEventsRequest, List<SelectEvent>>
    {

        private readonly IEventRepository _eventRepository;

        public EventsQueryHandler(
            IEventRepository eventRepository
        )
        {
            _eventRepository = eventRepository;
        }
        
        public async Task<SelectEvent?> Handle(GetEventRequest request, CancellationToken cancellationToken)
        {
            return await _eventRepository.GetByIdAsync(request.Id);
        }

        public async Task<List<SelectEvent>> Handle(GetEventsRequest request, CancellationToken cancellationToken)
        {
            return await _eventRepository.GetAllAsync(request.Id);
        }

        public async Task<List<SelectEvent>> Handle(GetPendingEventsRequest request, CancellationToken cancellationToken)
        {
           return await _eventRepository.GetAllAsync(request.Id, Enums.EStatus.PENDING);
        }
    }
}
