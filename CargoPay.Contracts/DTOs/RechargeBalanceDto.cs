

namespace CargoPay.Contracts.DTOs
{
    public class RechargeBalanceDto
    {
        public string CardNumber { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}
