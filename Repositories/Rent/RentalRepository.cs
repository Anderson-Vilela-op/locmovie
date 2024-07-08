using locmovie.Data;
using locmovie.Models.RentalModel;
using locmovie.Repositories.RentalRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace locmovie.Repositories.RentalRepository
{
    public class RentalRepository : IRentalRepository
    {
        private readonly AppDbContext _context;

        public RentalRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rental>> GetAllRentalsAsync()
        {
            return await _context.Rentals.ToListAsync();
        }

        public async Task<Rental?> GetRentalByIdAsync(int id)
        {
            return await _context.Rentals.FindAsync(id);
        }

        public async Task AddRentalAsync(Rental rental)
        {
            await _context.Rentals.AddAsync(rental);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }


    }
}