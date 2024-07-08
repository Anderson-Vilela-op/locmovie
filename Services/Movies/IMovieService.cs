using locmovie.Models.MovieModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace locmovie.Services.MovieService
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieListDto>> GetAllMoviesAsync();
        Task<MovieListDto?> GetMovieByIdAsync(int id);
        Task AddMovieAsync(MovieRegisterDto createMovieDto);
    }
}