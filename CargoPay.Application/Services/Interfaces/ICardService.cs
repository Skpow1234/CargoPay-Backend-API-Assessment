using CargoPay.Contracts.DTOs;

namespace CargoPay.Application.Services.Interfaces
{
    public interface ICardService
    {
        Task<CardDto> CreateCardAsync(CreateCardDto request);
        Task PayAsync(PaymentDto request);
        Task<decimal> GetCardBalanceByCardNumberAsync(string cardNumber);
        Task<decimal> GetCardBalanceByCardIdAsync(int id);
        Task<List<CardDto>> GetAllCardsAsync();
        Task<bool> RechargeBalanceAsync(RechargeBalanceDto request);
        Task<bool> DeleteCardAsync(int id);
    }
}
