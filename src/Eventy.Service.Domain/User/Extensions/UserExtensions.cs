using System.Security.Claims;

namespace Eventy.Service.Domain.User.Extensions
{
    public static class UserExtensions
    {

           public static string? GetUserId(this ClaimsPrincipal claimsPrincipal){
                return claimsPrincipal.Claims.FirstOrDefault(x => x.Type == Constants.USER_ID_CLAIM)?.Value;
           }
    }
}
