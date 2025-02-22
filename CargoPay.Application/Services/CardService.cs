using CargoPay.Contracts.DTOs;
using CargoPay.Application.Services.Interfaces;
using CargoPay.Infrastructure.Abstractions.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Memory;

namespace CargoPay.Application.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IPaymentFeeService _paymentFeeService;
        private readonly IMemoryCache _cache;

        public CardService(ICardRepository cardRepository, IPaymentFeeService paymentFeeService, IMemoryCache cache)
        {
            _cardRepository = cardRepository;
            _paymentFeeService = paymentFeeService;
            _cache = cache;
        }

        public async Task<CardDto> CreateCardAsync(CreateCardDto request)
        {
            return await _cardRepository.CreateCard(request);
        }

        public async Task<decimal> GetCardBalanceByCardNumberAsync(string cardNumber)
        {
            if (!_cache.TryGetValue(cardNumber, out decimal balance))
            {
                balance = await _cardRepository.GetCardBalanceByCardNumber(cardNumber);
                _cache.Set(cardNumber, balance, TimeSpan.FromMinutes(10)); // Cache for 10 minutes
            }
            return balance;
        }

        public async Task<decimal> GetCardBalanceByCardIdAsync(int id)
        {
            return await _cardRepository.GetCardBalanceByCardId(id);
        }

        public async Task<List<CardDto>> GetAllCardsAsync()
        {
            return await _cardRepository.GetAllCards();
        }

        public async Task PayAsync(PaymentDto request)
        {
            var feeRate = await _paymentFeeService.GetCurrentFeeRateAsync();
            var totalPayment = request.Amount + (request.Amount * feeRate);
            var cardBalance = await _cardRepository.GetCardBalanceByCardNumber(request.CardNumber);

            if (cardBalance < totalPayment)
                throw new InvalidOperationException("Insufficient balance.");

            await _cardRepository.UpdateCardBalance(request.CardNumber, cardBalance - totalPayment);
        }

        public async Task<bool> RechargeBalanceAsync(RechargeBalanceDto request)
        {
            return await _cardRepository.RechargeBalance(request);
        }

        public async Task<bool> DeleteCardAsync(int id)
        {
            return await _cardRepository.DeleteCard(id);
        }
    }
}
