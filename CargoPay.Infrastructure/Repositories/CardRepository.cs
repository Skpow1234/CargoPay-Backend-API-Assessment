using CargoPay.Contracts.DTOs;
using CargoPay.Domain.Entities;
using CargoPay.Infrastructure.Data;
using CargoPay.Infrastructure.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CargoPay.Infrastructure.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly AppDbContext _context;

        public CardRepository(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<CardDto> CreateCard(CreateCardDto request)
        {
            var card = new Card(request.CardNumber, request.Balance);

            _context.Cards.Add(card);
            await _context.SaveChangesAsync();

            return new CardDto
            {
                Id = card.Id,
                CardNumber = card.CardNumber,
                Balance = card.Balance,
                CreatedAt = card.CreatedAt
            };
        }

        public async Task<decimal> GetCardBalanceByCardNumber(string cardNumber)
        {
            var balance = await _context.Cards
                .Where(c => c.CardNumber == cardNumber)
                .Select(c => (decimal?)c.Balance) 
                .FirstOrDefaultAsync(); 

            return balance ?? throw new InvalidOperationException($"Card with card number: {cardNumber} not found!");
        }

        public async Task<decimal> GetCardBalanceByCardId(int id)
        {
            var balance = await _context.Cards
                .Where(c => c.Id == id)
                .Select(c => (decimal?)c.Balance)
                .FirstOrDefaultAsync();

            return balance ?? throw new InvalidOperationException($"Card with id: {id} not found!");
        }


        public async Task<List<CardDto>> GetAllCards()
        {
            return await _context.Cards
                .AsNoTracking()
                .Select(c => new CardDto
                {
                    Id = c.Id,
                    CardNumber = c.CardNumber,
                    Balance = c.Balance,
                    CreatedAt = c.CreatedAt
                })
                .ToListAsync();
        }

        public async Task UpdateCardBalance(string cardNumber, decimal newBalance)
        {
            int affectedRows = await _context.Cards
                .Where(c => c.CardNumber == cardNumber)
                .ExecuteUpdateAsync(s => s.SetProperty(x => x.Balance, newBalance));

            if (affectedRows == 0)
                throw new InvalidOperationException($"Card with card number: {cardNumber} not found!");
        }

        public async Task<bool> RechargeBalance(RechargeBalanceDto request)
        {
            var card = await _context.Cards
                .Where(c => c.CardNumber == request.CardNumber)
                .SingleOrDefaultAsync();

            if (card == null)
                throw new InvalidOperationException($"Card with card number: {request.CardNumber} not found!");

            card.Balance += request.Amount;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCard(int id)
        {
            var card = await _context.Cards
                .Where(c => c.Id == id)
                .SingleOrDefaultAsync();

            if (card == null)
                throw new InvalidOperationException($"Card with id: {id} not found!");

            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
