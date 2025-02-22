using CargoPay.Contracts.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CargoPay.Presentation.Controllers
{
    /// <summary>
    /// Handles authentication and token generation.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="configuration">Application configuration for JWT settings.</param>
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Authenticates a user and returns a JWT token if credentials are valid.
        /// </summary>
        /// <param name="request">Login request containing username and password.</param>
        /// <returns>A JWT token if authentication is successful, otherwise Unauthorized.</returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request payload.");
            }

            if (request.Username == "admin" && request.Password == "password")
            {
                var token = GenerateJwtToken(request.Username);
                return Ok(new { Token = token });
            }

            return Unauthorized("Invalid credentials!");
        }

        /// <summary>
        /// Generates a JWT token for an authenticated user.
        /// </summary>
        /// <param name="username">The username of the authenticated user.</param>
        /// <returns>A JWT token.</returns>
        private string GenerateJwtToken(string username)
        {
            var key = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(key))
            {
                throw new InvalidOperationException("JWT Key is not configured in appsettings.");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(ClaimTypes.Name, username),
        new Claim(ClaimTypes.Role, username == "admin" ? "Admin" : "User") 
    };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"] ?? "defaultIssuer",
                audience: _configuration["Jwt:Audience"] ?? "defaultAudience",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
