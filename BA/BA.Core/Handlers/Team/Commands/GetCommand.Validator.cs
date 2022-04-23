using FluentValidation;

namespace BA.Core.Handlers.Team.Commands;

public class GetCommandValidator : AbstractValidator<GetCommand>
{
    public GetCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}