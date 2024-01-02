using Eventy.Service.Domain.Entities;
using Eventy.Service.Domain.User.Models;

namespace Eventy.Service.Domain.User.Interfaces
{
    public interface IUserRepository
    {
        Task CreateAsync(UserEntityDomain userEntity);
        Task<UserEntityDomain?> GetByEmailAsync(string email);
        Task<List<SelectUser>> GetAllAsync();
    }
}
