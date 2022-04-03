using BA.Core.Models;
using MediatR;

namespace BA.Core.Commands.File;

public class UploadCommand : FileModel, IRequest<MemoryStream>
{
}