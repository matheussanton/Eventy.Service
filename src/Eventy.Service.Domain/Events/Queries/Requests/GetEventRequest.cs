using Eventy.Service.Domain.Bases.Requests;
using Eventy.Service.Domain.Events.Models;
using MediatR;

namespace Eventy.Service.Domain.Events.Queries.Requests
{
    public class GetEventRequest : ByIdRequest, IRequest<SelectEvent?>
    {
        public GetEventRequest(Guid id, Guid userId) : base(id){
            UserId = userId;
        }

        public Guid UserId { get; private set; }
    }
}
