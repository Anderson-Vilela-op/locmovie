using locmovie.Models.RentalModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace locmovie.Repositories.RentalRepository
{
    public interface IRentalRepository
    {
    Task<IEnumerable<Rental>> GetAllRentalsAsync();
    Task<Rental?> GetRentalByIdAsync(int id);
    Task AddRentalAsync(Rental rental);
    Task SaveChangesAsync();
    }
}