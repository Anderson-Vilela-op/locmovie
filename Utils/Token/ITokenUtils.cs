using System.Security.Claims;

namespace locmovie.Utils
{
    public interface ITokenUtils
    {
        string GenerateToken(Claim[] claims);
    }
}