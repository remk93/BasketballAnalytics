using FluentValidation;

namespace BA.Core.Commands.Person;

public class DeleteCommandValidator : AbstractValidator<DeleteCommand>
{
    public DeleteCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}