using FluentValidation;
using Genesis.Cdb.Challenge.Application.Commands;

namespace Genesis.Cdb.Challenge.Application.Validators
{ 

    public class CalculateCdbCommandValidator
        : AbstractValidator<CalculateCdbCommand>
    {
        public CalculateCdbCommandValidator()
        {
            RuleFor(x => x.InitialAmount)
                .GreaterThan(0)
                .WithMessage(
                    "Initial amount must be greater than zero.");

            RuleFor(x => x.Months)
                .GreaterThan(1)
                .WithMessage(
                    "Months must be greater than one.");
        }
    }

}
