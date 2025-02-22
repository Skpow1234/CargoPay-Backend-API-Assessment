

namespace CargoPay.Contracts.DTOs
{
    public class PaymentDto
    {
        public string CardNumber { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}
