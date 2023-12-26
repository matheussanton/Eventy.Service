using Eventy.Service.Domain.Bases.Requests;
using Eventy.Service.Domain.Entities;
using MediatR;

namespace Eventy.Service.Domain.Events.Queries.Requests
{
    public class GetEventsRequest : ByIdRequest, IRequest<List<EventEntityDomain>?>
    {
        public GetEventsRequest(Guid id) : base(id){}
    }
}
