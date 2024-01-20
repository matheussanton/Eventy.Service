using Eventy.Service.Domain.Entities;
using Eventy.Service.Domain.Enums;
using Eventy.Service.Domain.Events.Models;

namespace Eventy.Service.Domain.Events.Interfaces
{
    public interface IEventRepository
    {
        Task CreateAsync(EventEntityDomain eventEntity);
        Task UpdateAsync(EventEntityDomain eventEntity);
        Task UpdateStatusAsync(Guid eventId, Guid userId,  EStatus status);
        Task DeleteAsync(Guid id, Guid userId);
        Task<SelectEvent?> GetByIdAsync(Guid id, Guid userId);
        Task <List<SelectEvent>> GetAllAsync(Guid userId,  EStatus status = EStatus.ACTIVE);
    }
}
