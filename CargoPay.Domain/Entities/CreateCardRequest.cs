using System.ComponentModel.DataAnnotations;

namespace CargoPay.Domain.Entities
{
    public class CreateCardRequest
    {
        [Required, CreditCard, MaxLength(16)]
        public string CardNumber { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "Initial balance cannot be negative.")]
        public decimal Balance { get; set; }
    }
}
