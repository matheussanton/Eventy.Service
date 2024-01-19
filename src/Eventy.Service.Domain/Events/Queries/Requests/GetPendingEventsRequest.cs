using Eventy.Service.Domain.Bases.Requests;
using Eventy.Service.Domain.Events.Models;
using MediatR;

namespace Eventy.Service.Domain.Events.Queries.Requests
{
    public class GetPendingEventsRequest : ByIdRequest, IRequest<List<SelectEvent>?>
    {
        public GetPendingEventsRequest(Guid id) : base(id){}
    }
}
