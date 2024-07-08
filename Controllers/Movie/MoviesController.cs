using Microsoft.AspNetCore.Mvc;
using locmovie.Models.MovieModel;
using locmovie.Services.MovieService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace locmovie.Controllers.MoviesController
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly ILogger<MoviesController> _logger;

        public MoviesController(IMovieService movieService, ILogger<MoviesController> logger)
        {
            _movieService = movieService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            _logger.LogInformation("Retrieving all movies");
            try
            {
                var movies = await _movieService.GetAllMoviesAsync();
                return Ok(movies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving movies");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            _logger.LogInformation("Retrieving movie by ID: {Id}", id);
            try
            {
                var movie = await _movieService.GetMovieByIdAsync(id);
                if (movie == null) {
                    return NotFound();
                }
                return Ok(movie);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving movie with ID: {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddMovie([FromBody] MovieRegisterDto movieRegisterDto)
        {
            _logger.LogInformation("Adding new movie: {Title}", movieRegisterDto.Title);
            try
            {
                await _movieService.AddMovieAsync(movieRegisterDto);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Error adding movie: {Title}", movieRegisterDto.Title);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}