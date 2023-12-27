using Eventy.Service.Domain.User.Models;
using MediatR;

namespace Eventy.Service.Domain.Authentication.Commands
{
    public class AuthenticateRequest : IRequest<UserDTO?>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
