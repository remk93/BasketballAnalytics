using FluentValidation;

namespace BA.Core.Commands.Person;

public class GetCommandValidator : AbstractValidator<GetCommand>
{
    GetCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}