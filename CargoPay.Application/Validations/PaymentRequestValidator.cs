using CargoPay.Contracts.DTOs;
using FluentValidation;

namespace CargoPay.Application.Validations
{
    public class PaymentRequestValidator : AbstractValidator<PaymentDto>
    {
        public PaymentRequestValidator()
        {
            RuleFor(x => x.CardNumber)
                .NotEmpty()
                .WithMessage("Card number is required.")
                .Matches("^[0-9]{16}$") 
                .WithMessage("Card number must be exactly 16 digits.");

            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("Amount must be greater than 0.");
        }
    }
}
