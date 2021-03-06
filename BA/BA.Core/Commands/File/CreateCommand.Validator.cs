using FluentValidation;

namespace BA.Core.Commands.File;

public class CreateCommandValidator : AbstractValidator<CreateCommand>
{
    public CreateCommandValidator()
    {
        RuleFor(x => x.Id).Equal(0);
        RuleFor(x => x.Name).NotNull().NotEmpty();
        RuleFor(x => x.Link).NotNull().NotEmpty();
    }
}