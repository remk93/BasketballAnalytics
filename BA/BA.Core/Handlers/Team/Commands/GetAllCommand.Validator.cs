using FluentValidation;


namespace BA.Core.Handlers.Team.Commands;

public class GetAllCommandValidator : AbstractValidator<GetAllCommand>
{
    public GetAllCommandValidator()
    {
        RuleFor(x => x.FilterData).NotNull();
        RuleFor(x => x.FilterData.PageNumber).GreaterThan(0);
        RuleFor(x => x.FilterData.PageSize).GreaterThan(0);
    }
}