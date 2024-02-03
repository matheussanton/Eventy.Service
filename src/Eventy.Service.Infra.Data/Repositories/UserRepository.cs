using Eventy.Service.Domain.Entities;
using Eventy.Service.Domain.User.Interfaces;
using Eventy.Service.Domain.User.Models;
using Eventy.Service.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Eventy.Service.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private AppDbContext _context { get; }
        private ILogger<UserRepository> _logger { get; }

        public UserRepository(AppDbContext context,
                                ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CreateAsync(UserEntityDomain userEntity)
        {
           try
            {
                await _context.Users.AddAsync(userEntity);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR CREATING USER");
            }   
        }

        public async Task<List<SelectUser>> GetAllAsync()
        {
            try
            {
                var users = await _context.Users.ToListAsync();
                return users.Select(u => new SelectUser
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR GETTING ALL USERS");
                return null;
            }
        }

        public async Task<List<SelectUser>> GetByIdAsync(List<Guid> ids)
        {
            try
            {
                var users = await _context.Users.Where(x => ids.Contains(x.Id)).ToListAsync();
                return users.Select(u => new SelectUser
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR GETTING ALL USERS BY THEIR IDS");
                return null;
            }
        }

        public async Task<UserEntityDomain?> GetByEmailAsync(string email)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR GETTING USER BY EMAIL");
                return null;
            }
        }
    }
}
