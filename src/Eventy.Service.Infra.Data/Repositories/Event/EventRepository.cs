using Eventy.Service.Domain.Entities;
using Eventy.Service.Domain.Events.Interfaces;
using Eventy.Service.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Eventy.Service.Infra.Data.Repositories.Event
{
    public class EventRepository : IEventRepository
    {
        private AppDbContext _context { get; }
        private ILogger<EventRepository> _logger { get; }

        public EventRepository(AppDbContext context,
                                ILogger<EventRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CreateAsync(EventEntityDomain eventEntity)
        {
            try
            {
                _context.Events.Add(eventEntity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR CREATING EVENT");
            }        
        }

        public async Task UpdateAsync(EventEntityDomain eventEntity, EventEntityDomain record)
        {
            try
            {
                _context.Entry(record).CurrentValues.SetValues(eventEntity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR UPDATING EVENT");
            }
        }

        public async Task<EventEntityDomain?> GetByIdAsync(Guid id)
        {
            try
            {
                return await _context.Events
                            .FirstOrDefaultAsync(e => e.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR GETTING EVENT BY ID");
                return null;
            }
        }
    }
}
