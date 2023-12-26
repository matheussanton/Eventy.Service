using Eventy.Service.Domain.Entities;

namespace Eventy.Service.Domain.Events.Commands
{
    public class UpdateEventCommand : BaseEventCommand
    {
        public UpdateEventCommand(Guid id,
                                  string name,
                                  string description,
                                  DateTime date,
                                  string location,
                                  string googleMapsUrl,
                                  Guid? referenceId = null) : base(name, description, date, location, googleMapsUrl, referenceId)
        {
            Id = id;
        }

        public Guid Id { get; set; }

        public EventEntityDomain Parse(EventEntityDomain record)
        {
            return new EventEntityDomain(
                Name,
                Description,
                Date,
                Location,
                GoogleMapsUrl,
                record.CreatedBy,
                record.CreatedAt,
                ReferenceId,
                Id,
                updatedBy: UserId,
                updatedAt: DateTime.UtcNow.AddHours(-3)
            );
        }
    }
}
