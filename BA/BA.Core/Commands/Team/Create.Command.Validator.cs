using FluentValidation;

namespace BA.Core.Commands.Team;

public class CreateCommandValidator : AbstractValidator<CreateCommand>
{
    public CreateCommandValidator()
    {
        RuleFor(x => x.Id).Equal(0);
        RuleFor(x => x.Name).NotNull().NotEmpty();
        RuleFor(x => x.ShortName).NotNull().NotEmpty();
        RuleFor(x => x.City).NotNull().NotEmpty();
        RuleFor(x => x.Stadium).NotNull().NotEmpty();
        RuleFor(x => x.Founded).GreaterThan(1898);
    }
}