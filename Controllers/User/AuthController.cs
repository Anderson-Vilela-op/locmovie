using Microsoft.AspNetCore.Mvc;
using locmovie.Models;
using locmovie.Services;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;


namespace locmovie.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLogin)
        {
            _logger.LogInformation("Logging in user: {Email}", userLogin.Email);

            try
            {
                var token = await _authService.AuthenticateAsync(userLogin.Email, userLogin.Password);
                if (token == null)
                {
                    return Unauthorized();
                }
                _logger.LogWarning("Authentication failed for user: {Email}", userLogin.Email);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error logging in user: {Email}", userLogin.Email);
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegister)
        {
            var user = new User
            {
                Name = userRegister.Name,
                Email = userRegister.Email,
                Password = userRegister.Password
            };

            await _authService.RegisterAsync(user);
            return Ok();
        }
    }
}