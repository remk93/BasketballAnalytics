using BA.Core.Models;
using MediatR;

namespace BA.Core.Commands.File;

public class GetCommand : IRequest<FileModel>
{
    public int Id { get; set; }
}