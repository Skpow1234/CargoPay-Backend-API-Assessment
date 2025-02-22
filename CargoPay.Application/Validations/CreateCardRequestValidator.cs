using CargoPay.Contracts.DTOs;
using FluentValidation;

namespace CargoPay.Application.Validations
{
    public class CreateCardRequestValidator : AbstractValidator<CreateCardDto>
    {
        public CreateCardRequestValidator()
        {
            RuleFor(x => x.CardNumber)
                .NotEmpty()
                .WithMessage("Card number is required.")
                .Matches("^[0-9]{16}$") // Ensures exactly 16 digits
                .WithMessage("Card number must be exactly 16 digits.");

            RuleFor(x => x.Balance)
                .GreaterThan(0)
                .WithMessage("Balance must be greater than 0.");
        }
    }
}
