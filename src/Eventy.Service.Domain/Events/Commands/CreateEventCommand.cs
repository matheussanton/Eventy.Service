namespace Eventy.Service.Domain.Events.Commands
{
    public class CreateEventCommand : BaseEventCommand
    {
        public CreateEventCommand(
            string name,
            string description,
            DateTime date,
            string location,
            string googleMapsUrl,
            Guid? referenceId = null
        ) : base(name, description, date, location, googleMapsUrl, referenceId)
        {
        }
    }
}
