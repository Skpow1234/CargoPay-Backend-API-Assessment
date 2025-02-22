
namespace CargoPay.Contracts.DTOs
{
    public class CreateCardDto
    {
        public string CardNumber { get; set; } = string.Empty;
        public decimal Balance { get; set; }
    }
}
