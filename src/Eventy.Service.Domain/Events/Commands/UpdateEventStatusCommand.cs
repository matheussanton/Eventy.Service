using Eventy.Service.Domain.Entities;
using Eventy.Service.Domain.Enums;
using Eventy.Service.Domain.Events.Models;
using Flunt.Notifications;
using MediatR;

namespace Eventy.Service.Domain.Events.Commands
{
    public class UpdateEventStatusCommand : Notifiable<Notification>, IRequest
    {
        public UpdateEventStatusCommand(
            Guid eventId,
            Guid userId,
            EStatus status
        )
        {
            EventId = eventId;
            UserId = userId;
            Status = status;
        }
        public Guid EventId { get; set; }
        public Guid UserId { get; set; }
        public EStatus Status { get; set; }
    }
}
