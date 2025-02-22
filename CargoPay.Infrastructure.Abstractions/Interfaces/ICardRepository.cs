using CargoPay.Contracts.DTOs;

namespace CargoPay.Infrastructure.Abstractions.Interfaces
{
    public interface ICardRepository
    {
        Task<CardDto> CreateCard(CreateCardDto request);
        Task<decimal> GetCardBalanceByCardNumber(string cardNumber);
        Task<decimal> GetCardBalanceByCardId(int id);
        Task<List<CardDto>> GetAllCards();
        Task UpdateCardBalance(string cardNumber, decimal newBalance);
        Task<bool> RechargeBalance(RechargeBalanceDto request);
        Task<bool> DeleteCard(int id);
    }
}
