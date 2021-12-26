using FluentValidation;

namespace BA.Core.Commands.Team;
public class DeleteCommandvalidator : AbstractValidator<DeleteCommand>
{
    public DeleteCommandvalidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}