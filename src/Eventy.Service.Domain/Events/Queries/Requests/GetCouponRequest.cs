using Eventy.Service.Domain.Bases.Requests;
using Eventy.Service.Domain.Entities;
using MediatR;

namespace Eventy.Service.Domain.Events.Queries.Requests
{
    public class GetCouponRequest : ByIdRequest, IRequest<EventEntityDomain?>
    {
        public GetCouponRequest(Guid id) : base(id){}
    }
}
