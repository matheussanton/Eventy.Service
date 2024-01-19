using Flunt.Notifications;
using MediatR;

namespace Eventy.Service.Domain.Events.Commands
{
    public class DeleteEventCommand : Notifiable<Notification>, IRequest
    {
        public DeleteEventCommand(
            Guid id,
            Guid userId
        )
        {
            Id = id;
            UserId = userId;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
