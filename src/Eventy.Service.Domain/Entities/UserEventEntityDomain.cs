using System.ComponentModel.DataAnnotations.Schema;
using Eventy.Service.Domain.Enums;

namespace Eventy.Service.Domain.Entities
{
    [Table("user_event")]
    public class UserEventEntityDomain
    {
        public UserEventEntityDomain(
            Guid userId,
            Guid eventId,
            EStatus status = EStatus.PENDING
        )
        {
            UserId = userId;
            EventId = eventId;
            Status = status;
        }
        
        public Guid UserId { get; private set; }
        public Guid EventId { get; private set; }
        public EStatus Status { get; private set; }

        public void SetStatus(EStatus status)=> Status = status;

    }
}
