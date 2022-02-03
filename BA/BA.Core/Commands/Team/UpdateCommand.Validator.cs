using FluentValidation;

namespace BA.Core.Commands.Team;

public class UpdateCommandValidator : AbstractValidator<UpdateCommand>
{
    public UpdateCommandValidator() 
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Name).NotNull().NotEmpty();
        RuleFor(x => x.Code).NotNull().NotEmpty();
        RuleFor(x => x.City).NotNull().NotEmpty();
        RuleFor(x => x.Stadium).NotNull().NotEmpty();
        RuleFor(x => x.Founded).GreaterThan(1898);
    }
}