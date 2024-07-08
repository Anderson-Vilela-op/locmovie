using locmovie.Models.RentalModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace locmovie.Services.RentalService
{
    public interface IRentalService
    {
        Task<IEnumerable<RentalDto>> GetAllRentalsAsync();
        Task<RentalDto?> GetRentalByIdAsync(int id);
        Task AddRentalAsync(RentalRegisterDto rentalRegisterDto, int userId);
        
    }
}