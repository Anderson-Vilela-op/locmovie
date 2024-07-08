using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using locmovie.Models.RentalModel;
using locmovie.Services.RentalService;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace locmovie.Controllers.RentalController
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalService _rentalService;
        private readonly ILogger<RentalsController> _logger;

        public RentalsController(IRentalService rentalService, ILogger<RentalsController> logger)
        {
            _rentalService = rentalService ?? throw new ArgumentNullException(nameof(rentalService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllRentals()
        {
            _logger.LogInformation("Retrieving all rentals");
            try
            {
                var rentals = await _rentalService.GetAllRentalsAsync();
                return Ok(rentals);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving rentals");
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetRentalById(int id)
        {
            _logger.LogInformation("Retrieving rental by ID: {Id}", id);
            try
            {
                var rental = await _rentalService.GetRentalByIdAsync(id);
                if (rental == null)
                {
                    _logger.LogWarning("Rental not found with ID: {Id}", id);
                    return NotFound();
                }
                return Ok(rental);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving rental with ID: {Id}", id);
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddRental([FromBody] RentalRegisterDto rentalRegisterDto)
        {
            _logger.LogInformation("Adding new rental for movie ID: {MovieId}", rentalRegisterDto.MovieId);
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                {
                    _logger.LogError("User ID claim is missing or invalid");
                    return Unauthorized();
                }

                await _rentalService.AddRentalAsync(rentalRegisterDto, userId);
                return Ok();
            }
             catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding rental for movie ID: {MovieId}", rentalRegisterDto.MovieId);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}