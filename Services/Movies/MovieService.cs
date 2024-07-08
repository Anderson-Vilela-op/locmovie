using locmovie.Models.MovieModel;
using locmovie.Repositories.MovieRepository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace locmovie.Services.MovieService
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ILogger<MovieService> _logger;

        public MovieService(IMovieRepository movieRepository, ILogger<MovieService> logger)
        {
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }

        public async Task<IEnumerable<MovieListDto>> GetAllMoviesAsync()
        {
            _logger.LogInformation("Retrieving all movies");
            var movies = await _movieRepository.GetAllMoviesAsync();
            return movies.Select(m => new MovieListDto
            {
                Id = m.Id,
                Title = m.Title,
                Gender = m.Gender,
                Year = m.Year,
                Avaliable = m.Avaliable

            }).ToList();
        }

        public async Task<MovieListDto?> GetMovieByIdAsync(int id)
        {
            _logger.LogInformation("Retrieving movie by ID: {Id}", id);
            var movie = await _movieRepository.GetMovieByIdAsync(id);
            if (movie == null)
            {
                _logger.LogInformation("Movie not found with ID: {Id}", id);
                return null;
            }

            return new MovieListDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Gender = movie.Gender,
                Year = movie.Year,
                Avaliable = movie.Avaliable
            };
        }

        public async Task AddMovieAsync(MovieRegisterDto movieRegisterDto)
        {
            _logger.LogInformation("Adding new movie: {Title}", movieRegisterDto.Title);
            var movie = new Movie
            {
                Title = movieRegisterDto.Title,
                Gender = movieRegisterDto.Gender,
                Year = movieRegisterDto.Year,
                Avaliable = movieRegisterDto.Avaliable
            };

            await _movieRepository.AddMovieAsync(movie);
            await _movieRepository.SaveChangesAsync();

            _logger.LogInformation("Movie added: {Title}", movieRegisterDto.Title);
        }
    }
}