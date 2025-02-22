using System.ComponentModel.DataAnnotations;

namespace CargoPay.Domain.Entities
{
    public class Card
    {
        public int Id { get; set; }

        [Required, CreditCard, MaxLength(16)]
        public string CardNumber { get; private set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "Balance cannot be negative.")]
        public decimal Balance { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Card(string cardNumber, decimal balance)
        {
            CardNumber = cardNumber;
            Balance = balance;
        }
    }
}
