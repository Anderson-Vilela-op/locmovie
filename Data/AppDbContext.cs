using locmovie.Models;
using locmovie.Models.MovieModel;
using locmovie.Models.RentalModel;
using Microsoft.EntityFrameworkCore;


namespace locmovie.Data
{
    public class AppDbContext: DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {}

        public DbSet<User> Users {get;set;}
        public DbSet<Movie> Movies {get;set;}
        public DbSet<Rental> Rentals {get;set;}
    }
}