using System.Net;
using Eventy.Service.Domain.Hash;
using Eventy.Service.Domain.Hash.Interfaces;
using Eventy.Service.Domain.Responses;
using Eventy.Service.Domain.Responses.Enums;
using Eventy.Service.Domain.User.Interfaces;
using MediatR;

namespace Eventy.Service.Domain.User.Commands.Handler
{
    public class UserHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly Response _response;

        public UserHandler(
            IUserRepository userRepository,
            Response response
        )
        {
            _userRepository = userRepository;
            _response = response;
        }


        public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            request.Password = PasswordHasher.Hash(request.Password);

            var record = await _userRepository.GetByEmailAsync(request.Email);
            if (record != null)
            {
                request.AddNotification("Email", "Email já cadastrado");
                _response.Send(ResponseStatus.Fail, HttpStatusCode.BadRequest, request.Notifications);
                return;
            }
            
            var user = request.Parse();
            await _userRepository.CreateAsync(user);

            return;
        }
    }
}
