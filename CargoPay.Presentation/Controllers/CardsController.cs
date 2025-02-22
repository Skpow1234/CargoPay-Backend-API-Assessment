using CargoPay.Application.Services.Interfaces;
using CargoPay.Contracts.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CargoPay.Presentation.Controllers
{
    /// <summary>
    /// Controller for managing cards, including creation, payments, balance inquiries, and deletion.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;
        private readonly IValidator<RechargeBalanceDto> _rechargeBalanceValidator;
        private readonly IValidator<PaymentDto> _paymentValidator;
        private readonly IValidator<CreateCardDto> _createCardValidator;

        /// <summary>
        /// Initializes a new instance of the <see cref="CardsController"/> class.
        /// </summary>
        /// <param name="cardService">Service for card operations.</param>
        /// <param name="rechargeBalanceValidator">Validator for recharge balance requests.</param>
        /// <param name="paymentValidator">Validator for payment requests.</param>
        /// <param name="createCardValidator">Validator for card creation requests.</param>
        public CardsController(
            ICardService cardService,
            IValidator<RechargeBalanceDto> rechargeBalanceValidator,
            IValidator<PaymentDto> paymentValidator,
            IValidator<CreateCardDto> createCardValidator)
        {
            _cardService = cardService;
            _rechargeBalanceValidator = rechargeBalanceValidator;
            _paymentValidator = paymentValidator;
            _createCardValidator = createCardValidator;
        }

        /// <summary>
        /// Creates a new card with an initial balance.
        /// </summary>
        /// <param name="request">Card creation request containing card number and initial balance.</param>
        /// <returns>Response indicating success or failure.</returns>
        [HttpPost("create")]
        public async Task<ActionResult> CreateCard(CreateCardDto request)
        {
            var validationResult = await _createCardValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(new { Message = "Validation error!", Errors = validationResult.Errors.Select(x => x.ErrorMessage) });
            }

            var createdCard = await _cardService.CreateCardAsync(request);

            return Ok(new { Message = "Card created successfully!", Card = createdCard });
        }

        /// <summary>
        /// Processes a payment and applies a fee to the amount.
        /// </summary>
        /// <param name="request">Payment request containing card number and amount.</param>
        /// <returns>Response indicating success or failure.</returns>
        [HttpPost("pay")]
        public async Task<ActionResult> Pay(PaymentDto request)
        {
            var validationResult = await _paymentValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(new { Message = "Validation error!", Errors = validationResult.Errors.Select(x => x.ErrorMessage) });
            }

            await _cardService.PayAsync(request);

            return Ok(new { Message = "Payment was successful!" });
        }

        /// <summary>
        /// Retrieves the balance of a card using the card number.
        /// </summary>
        /// <param name="cardNumber">The card number to retrieve the balance for.</param>
        /// <returns>The balance of the card.</returns>
        [HttpGet("by-number/{cardNumber}/balance")]
        public async Task<ActionResult> GetCardBalanceByCardNumber(string cardNumber)
        {
            var balance = await _cardService.GetCardBalanceByCardNumberAsync(cardNumber);
            return Ok(new { Balance = balance });
        }

        /// <summary>
        /// Retrieves the balance of a card using the card ID.
        /// </summary>
        /// <param name="id">The card ID.</param>
        /// <returns>The balance of the card.</returns>
        [HttpGet("by-id/{id}/balance")]
        public async Task<ActionResult> GetCardBalanceByCardId(int id)
        {
            var balance = await _cardService.GetCardBalanceByCardIdAsync(id);
            return Ok(new { Balance = balance });
        }

        /// <summary>
        /// Retrieves all registered cards.
        /// </summary>
        /// <returns>A list of all registered cards.</returns>
        [HttpGet("cards")]
        public async Task<ActionResult<List<CardDto>>> GetAllCards()
        {
            var cards = await _cardService.GetAllCardsAsync();
            return Ok(cards);
        }

        /// <summary>
        /// Recharges the balance of a specified card.
        /// </summary>
        /// <param name="request">Recharge request containing card number and amount.</param>
        /// <returns>Response indicating success or failure.</returns>
        [HttpPost("recharge-balance")]
        public async Task<ActionResult> RechargeBalance(RechargeBalanceDto request)
        {
            var validationResult = await _rechargeBalanceValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(new { Message = "Validation error!", Errors = validationResult.Errors.Select(x => x.ErrorMessage) });
            }

            await _cardService.RechargeBalanceAsync(request);

            return Ok(new { Message = "Balance recharged successfully!" });
        }

        /// <summary>
        /// Deletes a card based on its ID.
        /// </summary>
        /// <param name="id">The ID of the card to be deleted.</param>
        /// <returns>Response indicating success or failure.</returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCard(int id)
        {
            await _cardService.DeleteCardAsync(id);
            return Ok(new { Message = "Card deleted successfully!" });
        }

    }
}
