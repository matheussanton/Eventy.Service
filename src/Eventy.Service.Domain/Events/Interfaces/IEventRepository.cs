using Eventy.Service.Domain.Entities;
using Eventy.Service.Domain.Events.Models;

namespace Eventy.Service.Domain.Events.Interfaces
{
    public interface IEventRepository
    {
        Task CreateAsync(EventEntityDomain eventEntity);
        Task UpdateAsync(EventEntityDomain eventEntity);
        Task DeleteAsync(Guid id, Guid userId);
        Task<SelectEvent?> GetByIdAsync(Guid id);
        Task <List<SelectEvent>> GetAllAsync(Guid userId);
    }
}
