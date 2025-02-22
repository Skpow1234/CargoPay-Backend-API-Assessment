using System.ComponentModel.DataAnnotations;

namespace CargoPay.Domain.Entities
{
    public class LoginRequest
    {
        [Required, MinLength(3)]
        public string Username { get; set; } = string.Empty;

        [Required, MinLength(6)]
        public string Password { get; set; } = string.Empty;
    }
}
