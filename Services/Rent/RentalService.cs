using locmovie.Models.RentalModel;
using locmovie.Repositories;
using locmovie.Repositories.MovieRepository;
using locmovie.Repositories.RentalRepository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace locmovie.Services.RentalService
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<RentalService> _logger;

        public RentalService(IRentalRepository rentalRepository, IMovieRepository movieRepository, IUserRepository userRepository, ILogger<RentalService> logger)
        {
            _rentalRepository = rentalRepository ?? throw new ArgumentNullException(nameof(rentalRepository));
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<RentalDto>> GetAllRentalsAsync()
        {
            _logger.LogInformation("Retrieving all rentals");
            var rentals = await _rentalRepository.GetAllRentalsAsync();
            return rentals.Select(r => new RentalDto
            {
                Id = r.Id,
                MovieId = r.MovieId,
                UserId = r.UserId,
                RentalDate = r.RentalDate,
                DueDate = r.RentalDate
            }).ToList();
        }

        public async Task<RentalDto?> GetRentalByIdAsync(int id)
        {
            _logger.LogInformation("Retrieving rental by ID: {Id}", id);
            var rental = await _rentalRepository.GetRentalByIdAsync(id);
            if (rental == null)
            {
                _logger.LogWarning("Rental not found with ID: {Id}", id);
                return null;
            }
            return new RentalDto
            {
                Id = rental.Id,
                MovieId = rental.MovieId,
                UserId = rental.UserId,
                RentalDate = rental.RentalDate,
                DueDate = rental.DueDate
            };
        }

        public async Task AddRentalAsync(RentalRegisterDto rentalRegisterDto, int userId)
        {
            _logger.LogInformation("Adding new rental for movie ID: {MovieId}", rentalRegisterDto.MovieId);
            var movie = await _movieRepository.GetMovieByIdAsync(rentalRegisterDto.MovieId);
            if (movie == null)
            {
                _logger.LogError("Movie with ID: {MovieId} not found", rentalRegisterDto.MovieId);
                throw new KeyNotFoundException($"Movie with ID {rentalRegisterDto.MovieId} not found");
            }
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                _logger.LogError("User with ID: {UserId} not found", userId);
                throw new KeyNotFoundException($"User with ID {userId} not found");
            }

            if (!movie.Avaliable)
            {
                _logger.LogError("Movie with ID: {MovieId} is not available for rental", rentalRegisterDto.MovieId);
                throw new InvalidOperationException($"Movie with ID {rentalRegisterDto.MovieId} is not available for rental");
            }

            var rental = new Rental
            {
                MovieId = rentalRegisterDto.MovieId,
                UserId = userId,
                RentalDate = rentalRegisterDto.RentalDate,
                DueDate = rentalRegisterDto.DueDate
            };

            movie.Avaliable = false;


            await _rentalRepository.AddRentalAsync(rental);
            await _rentalRepository.SaveChangesAsync();

            _logger.LogInformation("Rental added for movie ID: {MovieId}", rentalRegisterDto.MovieId);

        }
    }
}