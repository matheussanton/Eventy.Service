using Eventy.Service.Domain.Entities;

namespace Eventy.Service.Domain.Events.Interfaces
{
    public interface IEventRepository
    {
        Task CreateAsync(EventEntityDomain eventEntity);
        Task UpdateAsync(EventEntityDomain eventEntity, EventEntityDomain record);
        Task<EventEntityDomain?> GetByIdAsync(Guid id);
        Task <List<EventEntityDomain>> GetAllAsync(Guid userId);
    }
}
