using System.Threading.Tasks;
using locmovie.Models;

namespace locmovie.Services
{
    public interface IAuthService
    {
        Task<string?> AuthenticateAsync(string email, string password);
        Task RegisterAsync(User user);
    }
}