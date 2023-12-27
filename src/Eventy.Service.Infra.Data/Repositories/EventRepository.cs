using Eventy.Service.Domain.Entities;
using Eventy.Service.Domain.Events.Interfaces;
using Eventy.Service.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Eventy.Service.Infra.Data.Repositories
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
                await _context.Events.AddAsync(eventEntity);

                await _context.UserEvents.AddRangeAsync(eventEntity.UserEvents);

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

                var userEventsToRemove = record.UserEvents.Where(x => !eventEntity.UserEvents.Any(y => y.UserId == x.UserId));
                _context.UserEvents.RemoveRange(userEventsToRemove);

                var userEventsToAdd = eventEntity.UserEvents.Where(x => !record.UserEvents.Any(y => y.UserId == x.UserId));
                await _context.UserEvents.AddRangeAsync(userEventsToAdd);

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
                var eventEntity = await _context.Events
                            .FirstOrDefaultAsync(e => e.Id == id && !e.Deleted);

                if (eventEntity == null)
                    return null;

                var userEvents = await _context.UserEvents
                            .Where(ue => ue.EventId == eventEntity.Id
                                        && ue.UserId != eventEntity.CreatedBy)
                            .ToListAsync();

                eventEntity.UserEvents = userEvents;

                return eventEntity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR GETTING EVENT BY ID");
                return null;
            }
        }

        public async Task<List<EventEntityDomain>> GetAllAsync(Guid userId)
        {
            try
            {
              return _context.Events
                            .Join(
                                _context.UserEvents,
                                a => a.Id,
                                b => b.EventId,
                                (a, b) => new { Event = a, UserEvent = b }
                            )
                            .Where(joinResult => joinResult.UserEvent.UserId == userId
                                                && !joinResult.Event.Deleted)
                            .Select(joinResult => joinResult.Event)
                            .ToList();
                            
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR GETTING ALL EVENTS");
                return null;
            }
        }
    }
}
