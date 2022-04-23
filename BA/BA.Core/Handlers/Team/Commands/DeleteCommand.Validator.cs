using FluentValidation;

namespace BA.Core.Handlers.Team.Commands;
public class DeleteCommandvalidator : AbstractValidator<DeleteCommand>
{
    public DeleteCommandvalidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}