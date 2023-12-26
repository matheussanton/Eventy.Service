using Eventy.Service.Domain.Bases.Requests;
using Eventy.Service.Domain.Entities;
using MediatR;

namespace Eventy.Service.Domain.Events.Queries.Requests
{
    public class GetEventRequest : ByIdRequest, IRequest<EventEntityDomain?>
    {
        public GetEventRequest(Guid id) : base(id){}
    }
}
