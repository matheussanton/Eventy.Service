using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Eventy.Service.Domain.Hash;
using Eventy.Service.Domain.Hash.Interfaces;
using Eventy.Service.Domain.Settings;
using Eventy.Service.Domain.User.Interfaces;
using Eventy.Service.Domain.User.Models;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Eventy.Service.Domain.Authentication.Commands.Handler
{
    public class AuthenticateHandler : IRequestHandler<AuthenticateRequest, UserDTO?>
    {
        private readonly IUserRepository _userRepository;
        // private readonly IPasswordHasher _passwordHasher;
        private readonly AppSettings _appSettings;

        public AuthenticateHandler(
            IUserRepository userRepository,
            // IPasswordHasher passwordHasher,
            AppSettings appSettings
        )
        {
            _userRepository = userRepository;
            // _passwordHasher = passwordHasher;
            _appSettings = appSettings;
        }


        public async Task<UserDTO?> Handle(AuthenticateRequest request, CancellationToken cancellationToken)
        {
            var userRecord = await _userRepository.GetByEmailAsync(request.Email);

            if (userRecord == null) return null;

            // if(_passwordHasher.Verify(request.Password, userRecord.Password))
            if(PasswordHasher.Verify(request.Password, userRecord.Password))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                    var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.JwtSecretKey));
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Email, request.Email),
                            new Claim(Constants.USER_ID_CLAIM, userRecord.Id.ToString())
                        }),
                        Expires = DateTime.UtcNow.AddHours(3),
                        SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var tokenString = String.Concat("Bearer ", tokenHandler.WriteToken(token));
                    
                return new UserDTO
                {
                    Email = userRecord.Email,
                    Name = userRecord.Name,
                    Token = tokenString
                };
            }

            return null;
        }
    }
    
}
