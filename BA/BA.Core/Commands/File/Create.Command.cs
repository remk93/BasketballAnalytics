using BA.Core.Models;
using MediatR;

namespace BA.Core.Commands.File;

public class CreateCommand : IRequest<FileModel>
{
    public string Name { get; set; } = default!;
    public string Link { get; set; } = default!;
}