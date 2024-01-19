using Eventy.Service.Domain.Entities;

namespace Eventy.Service.Domain.User.Models
{
    public class SelectUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
