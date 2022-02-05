using FluentValidation;

namespace BA.Core.Commands.Team;

public class UpdateCommandValidator : AbstractValidator<UpdateCommand>
{
    public UpdateCommandValidator() 
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Name).NotNull().NotEmpty();
        RuleFor(x => x.Code).NotNull().NotEmpty();
        When(x => !string.IsNullOrWhiteSpace(x.Code), () =>
        {
            RuleFor(x => x.Code.Length).LessThan(4).GreaterThan(2).WithMessage("Code should be less then 4 symbols and greater than 1");
        });
        RuleFor(x => x.City).NotNull().NotEmpty();
        RuleFor(x => x.Stadium).NotNull().NotEmpty();
        RuleFor(x => x.Founded).GreaterThan(1898);
    }
}