using FluentValidation;

namespace BA.Core.Commands.File;

public class DeleteCommandValidator : AbstractValidator<DeleteCommand>
{
    public DeleteCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Link).NotNull().NotEmpty();
    }
}