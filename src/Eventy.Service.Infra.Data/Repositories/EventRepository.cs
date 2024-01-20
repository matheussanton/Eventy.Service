using Eventy.Service.Domain.Entities;
using Eventy.Service.Domain.Enums;
using Eventy.Service.Domain.Events.Interfaces;
using Eventy.Service.Domain.Events.Models;
using Eventy.Service.Domain.User.Models;
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

        public async Task UpdateAsync(EventEntityDomain eventEntity)
        {
            try
            {
                var record = await _context.Events.FirstOrDefaultAsync(x => x.Id == eventEntity.Id);
                if(record == null) return;

                _context.Entry(record).CurrentValues.SetValues(eventEntity);

                var userEventsToRemove = record.UserEvents.Where(x => !eventEntity.UserEvents.Any(y => y.UserId == x.UserId) && x.UserId != eventEntity.CreatedBy);
                _context.UserEvents.RemoveRange(userEventsToRemove);

                var userEventsToAdd = eventEntity.UserEvents.Where(x => !record.UserEvents.Any(y => y.UserId == x.UserId) && x.UserId != eventEntity.CreatedBy);
                await _context.UserEvents.AddRangeAsync(userEventsToAdd);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR UPDATING EVENT");
            }
        }

        public async Task UpdateStatusAsync(Guid eventId, Guid userId, EStatus status)
        {
             try
            {
                var userEvent = await _context.UserEvents
                                      .FirstOrDefaultAsync(x => x.EventId == eventId && x.UserId == userId);

                userEvent?.SetStatus(status);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR UPDATING USER EVENT STATUS");
            }
        }

        public async Task<SelectEvent?> GetByIdAsync(Guid id, Guid userId)
        {
            try
            {
                var evento = await _context.Events
                                .Where(x => x.Id == id)
                                .FirstOrDefaultAsync();

                if(evento == null)
                    return null;

                var users = _context.Users
                            .Join(
                                _context.UserEvents,
                                a => a.Id,
                                b => b.UserId,
                                (a, b) => new { User = a, UserEvent = b }
                            )
                            .Where(joinResult => joinResult.UserEvent.EventId == id)
                            .Select(joinResult => new {joinResult.User, joinResult.UserEvent})
                            .ToList();

                var selectEvent = new SelectEvent
                {
                    Id = evento.Id,
                    Name = evento.Name,
                    Description = evento.Description,
                    StartDate = evento.StartDate,
                    EndDate = evento.EndDate,
                    Location = evento.Location,
                    GoogleMapsUrl = evento.GoogleMapsUrl,
                    IsOwner = evento.CreatedBy == userId,
                    CreatedAt = evento.CreatedAt,
                    CreatedBy = evento.CreatedBy,
                    Participants = new List<SelectUser>()
                };

                foreach (var item in users)
                {
                    if(item.UserEvent.UserId == evento.CreatedBy)
                    {
                        selectEvent.Owner = new SelectUser
                        {
                            Id = item.User.Id,
                            Name = item.User.Name,
                            Email = item.User.Email
                        };

                        continue;
                    }
                    
                    selectEvent.Participants.Add(new SelectUser
                    {
                        Id = item.User.Id,
                        Name = item.User.Name,
                        Email = item.User.Email
                    });
                }

                return selectEvent;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR GETTING EVENT BY ID");
                return null;
            }
        }

        public async Task<List<SelectEvent>> GetAllAsync(Guid userId, EStatus status = EStatus.ACTIVE)
        {
            try
            {
                // TODOS EVENTOS QUE O USUARIO PARTICIPA
                var eventIds = _context.UserEvents
                                .Where(ue => ue.UserId == userId && ue.Status == status)
                                .Select(ue => ue.EventId);

                var events = _context.Events
                            .Join(
                                _context.UserEvents,
                                a => a.Id,
                                b => b.EventId,
                                (a, b) => new { Event = a, UserEvent = b }
                            )
                            .Join(
                                _context.Users,
                                a => a.UserEvent.UserId,
                                b => b.Id,
                                (a, b) => new { Event = a.Event, UserEvent = a.UserEvent, User = b }
                            )
                            .Where(joinResult => eventIds.Contains(joinResult.Event.Id))
                            .Select(joinResult => new {joinResult.Event, joinResult.UserEvent, joinResult.User})
                            .ToList();
                

                
                var dictionary  = new Dictionary<Guid, SelectEvent>();

                foreach (var item in events)
                {
                    if(!dictionary.TryGetValue(item.Event.Id, out var selectEvent))
                    {
                        selectEvent = new SelectEvent
                        {
                            Id = item.Event.Id,
                            Name = item.Event.Name,
                            Description = item.Event.Description,
                            StartDate = item.Event.StartDate,
                            EndDate = item.Event.EndDate,
                            Location = item.Event.Location,
                            GoogleMapsUrl = item.Event.GoogleMapsUrl,
                            IsOwner = item.Event.CreatedBy == userId,
                            Participants = new List<SelectUser>()
                        };

                        dictionary.Add(item.Event.Id, selectEvent);
                    }

                    // IDENTIFICA O DONO DO EVENTO
                    if(item.UserEvent.UserId == item.Event.CreatedBy)
                    {
                        selectEvent.Owner = new SelectUser
                        {
                            Id = item.User.Id,
                            Name = item.User.Name,
                            Email = item.User.Email
                        };

                        continue;
                    }
                    
                    
                    selectEvent.Participants.Add(new SelectUser
                    {
                        Id = item.User.Id,
                        Name = item.User.Name,
                        Email = item.User.Email
                    });
                }

                return dictionary.Values.ToList();
                            
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR GETTING ALL EVENTS");
                return null;
            }
        }

        public async Task DeleteAsync(Guid id, Guid userId)
        {
            try
            {
                var record = await _context.Events.FirstOrDefaultAsync(x => x.Id == id);
                if(record == null) return;

                record.SetDeleted(userId, DateTime.UtcNow.AddHours(-3));

                var userEventsToRemove = _context.UserEvents.Where(x => x.EventId == id);
                _context.UserEvents.RemoveRange(userEventsToRemove);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR DELETING EVENT");
            }
        }
    }
}
