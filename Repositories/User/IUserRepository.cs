using locmovie.Models;
using System.Threading.Tasks;

namespace locmovie.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task AddUserAsync(User user);
        Task<User?> GetUserByIdAsync(int id);

    }
}