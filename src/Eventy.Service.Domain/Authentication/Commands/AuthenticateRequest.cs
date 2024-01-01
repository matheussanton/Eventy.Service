using Eventy.Service.Domain.User.Models;
using Flunt.Notifications;
using MediatR;

namespace Eventy.Service.Domain.Authentication.Commands
{
    public class AuthenticateRequest : Notifiable<Notification>, IRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
