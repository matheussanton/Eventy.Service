using Eventy.Service.Domain.Hash.Interfaces;
using Eventy.Service.Domain.User.Interfaces;
using MediatR;

namespace Eventy.Service.Domain.User.Commands.Handler
{
    public class UserHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UserHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher
        )
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }


        public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            request.Password = _passwordHasher.Hash(request.Password);
            
            var user = request.Parse();
            await _userRepository.CreateAsync(user);

            return;
        }
    }
}
