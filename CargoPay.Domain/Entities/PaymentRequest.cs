using System.ComponentModel.DataAnnotations;

namespace CargoPay.Domain.Entities
{
    public class PaymentRequest
    {
        [Required, CreditCard, MaxLength(16)]
        public string CardNumber { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }
    }
}
