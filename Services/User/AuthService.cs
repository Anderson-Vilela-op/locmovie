using locmovie.Models;
using locmovie.Repositories;
using locmovie.Utils;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using System;

namespace locmovie.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenUtils _tokenUtils;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IUserRepository userRepository, ITokenUtils tokenUtils, ILogger<AuthService> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _tokenUtils = tokenUtils ?? throw new ArgumentNullException(nameof(tokenUtils));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<string?> AuthenticateAsync(string email, string password)
        {
            _logger.LogInformation("Authenticating user: {Email}", email);

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                _logger.LogError("Email and password cannot be null or empty");
                throw new ArgumentException("Email and password cannot be null or empty");
            }

            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null || user.Password != password) // Adicione hashing de senha na produção
            {
                _logger.LogWarning("Authentication failed for user: {Email}", email);
                return null;
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var token = _tokenUtils.GenerateToken(claims);
            _logger.LogInformation("Token generated for user: {Email}", email);

            return token;
        }

        public async Task RegisterAsync(User user)
        {
            if (user == null)
            {
                _logger.LogError("User cannot be null");
                throw new ArgumentNullException(nameof(user));
            }

            _logger.LogInformation("Registering user: {Email}", user.Email);

            // Adicione lógica para hashing de senha
            await _userRepository.AddUserAsync(user);

            _logger.LogInformation("User registered: {Email}", user.Email);
        }
    }
}
