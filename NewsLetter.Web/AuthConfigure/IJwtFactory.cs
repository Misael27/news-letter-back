using NewsLetter.Core.Entities;
using System.Security.Claims;

namespace NewsLetter.Web.AuthConfigure
{
    public interface IJwtFactory
    {
        Task<AccessTokenDTO> GenerateEncodedToken(string userName, ClaimsIdentity identity);
        ClaimsIdentity GenerateClaimsIdentity(User user, IList<string> userRoles);
    }
}
