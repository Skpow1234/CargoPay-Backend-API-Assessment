
namespace CargoPay.Contracts.DTOs
{
    public class CardDto
    {
        public int Id { get; set; }
        public string CardNumber { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
