using FluentValidation;

namespace BA.Core.Commands.Team;

public class GetCommandValidator : AbstractValidator<GetCommand>
{
    public GetCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}