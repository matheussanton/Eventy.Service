using Eventy.Service.Domain.Bases.Requests;
using Eventy.Service.Domain.Entities;
using Eventy.Service.Domain.Events.Models;
using MediatR;

namespace Eventy.Service.Domain.Events.Queries.Requests
{
    public class GetEventsRequest : ByIdRequest, IRequest<List<SelectEvent>?>
    {
        public GetEventsRequest(Guid id) : base(id){}
    }
}
