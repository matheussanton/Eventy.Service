namespace Eventy.Service.Domain.Bases.Requests
{
    public class ByIdRequest
    {
        public ByIdRequest(Guid id)
        {
            Id = id;
        }
        
        public Guid Id { get; private set; }
    }
}
