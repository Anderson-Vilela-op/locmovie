using locmovie.Models.MovieModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace locmovie.Repositories.MovieRepository
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAllMoviesAsync();
        Task<Movie?> GetMovieByIdAsync(int id);
        Task AddMovieAsync(Movie movie);
        Task SaveChangesAsync();
        
    }
}