using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Eventy.Service.Domain.Hash;
using Eventy.Service.Domain.Responses;
using Eventy.Service.Domain.Responses.Enums;
using Eventy.Service.Domain.Settings;
using Eventy.Service.Domain.User.Interfaces;
using Eventy.Service.Domain.User.Models;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Eventy.Service.Domain.Authentication.Commands.Handler
{
    public class AuthenticateHandler : IRequestHandler<AuthenticateRequest>
    {
        private readonly IUserRepository _userRepository;
        private readonly AppSettings _appSettings;
        private readonly Response<UserDTO> _response;

        public AuthenticateHandler(
            IUserRepository userRepository,
            AppSettings appSettings,
            Response<UserDTO> response
        )
        {
            _userRepository = userRepository;
            _appSettings = appSettings;
            _response = response;
        }


        public async Task Handle(AuthenticateRequest request, CancellationToken cancellationToken)
        {
            var userRecord = await _userRepository.GetByEmailAsync(request.Email);

            if (userRecord == null) {
                request.AddNotification("User", "Usuário ou senha inválidos");
                _response.Send(ResponseStatus.Fail, null, HttpStatusCode.BadRequest, request.Notifications);
                return;
            };

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
                    
                var userDTO =  new UserDTO
                {
                    Email = userRecord.Email,
                    Name = userRecord.Name,
                    Token = tokenString
                };

                _response.Send(ResponseStatus.Success, userDTO, HttpStatusCode.OK);
                return;
            }

            request.AddNotification("User", "Usuário ou senha inválidos");
            _response.Send(ResponseStatus.Fail, null, HttpStatusCode.BadRequest, request.Notifications);
        }

    }
    
}
