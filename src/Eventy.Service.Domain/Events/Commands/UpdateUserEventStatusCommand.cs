using Eventy.Service.Domain.Enums;
using Flunt.Notifications;
using MediatR;

namespace Eventy.Service.Domain.Events.Commands
{
    public class UpdateUserEventStatusCommand : Notifiable<Notification>, IRequest
    {
        public UpdateUserEventStatusCommand(
            Guid userId,
            Guid eventId,
            EStatus status
        )
        {
            UserId = userId;
            EventId = eventId;
            Status = status;
        }
        
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }
        public EStatus Status { get; private set; }
        public void SetStatus(EStatus status) => Status = status;
    }
}
