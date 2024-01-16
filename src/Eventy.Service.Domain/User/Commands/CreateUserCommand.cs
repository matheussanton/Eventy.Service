using System.Text.Json.Serialization;
using Eventy.Service.Domain.Entities;
using Eventy.Service.Domain.User.Enums;
using Flunt.Notifications;
using MediatR;

namespace Eventy.Service.Domain.User.Commands
{
    public class CreateUserCommand :  Notifiable<Notification>, IRequest

    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [JsonIgnore]
        public EUserRole Role { get; } = EUserRole.CUSTOMER;


        public UserEntityDomain Parse(){
            return new UserEntityDomain(
                name: Name,
                email: Email,
                password: Password,
                role: Role,
                DateTime.UtcNow.AddHours(-3),
                Guid.Parse(Constants.DEMO_USER_ID)
            );
        }
    }
}
