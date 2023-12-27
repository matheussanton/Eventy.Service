using Eventy.Service.Domain.Entities;

namespace Eventy.Service.Domain.User.Interfaces
{
    public interface IUserRepository
    {
        Task CreateAsync(UserEntityDomain userEntity);
        Task<UserEntityDomain?> GetByEmailAsync(string email);
        Task<List<UserEntityDomain>> GetAllAsync();
    }
}
