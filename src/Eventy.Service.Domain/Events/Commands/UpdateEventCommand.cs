using Eventy.Service.Domain.Entities;
using Eventy.Service.Domain.Events.Models;

namespace Eventy.Service.Domain.Events.Commands
{
    public class UpdateEventCommand : BaseEventCommand
    {
        public UpdateEventCommand(Guid id,
                                  string name,
                                  string description,
                                  DateTime startDate,
                                  DateTime endDate,
                                  string location,
                                  string googleMapsUrl) : base(name, description, startDate, endDate, location, googleMapsUrl)
        {
            Id = id;
        }

        public Guid Id { get; set; }

        public EventEntityDomain Parse(SelectEvent record)
        {
             var entity = new EventEntityDomain(
                Name,
                Description,
                StartDate,
                EndDate,
                Location,
                GoogleMapsUrl,
                record.CreatedBy,
                record.CreatedAt,
                Id,
                updatedBy: UserId,
                updatedAt: DateTime.UtcNow.AddHours(-3)
            );

            entity.UserEvents.Add(new UserEventEntityDomain(record.CreatedBy, entity.Id));
            Participants.ForEach(x => entity.UserEvents.Add(new UserEventEntityDomain(x, entity.Id)));

            return entity;
        }
    }
}
