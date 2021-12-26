using FluentValidation;

namespace BA.Core.Commands.File;

public class DownloadCommandValidator : AbstractValidator<DownloadCommand>
{
    public DownloadCommandValidator()
    {
        RuleFor(x => x.File).Must(x => x != null && x.Length > 0).WithMessage("File can't be empty");
    }
}