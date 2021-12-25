using BA.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BA.Core.Commands.File;

public class DownloadCommand : IRequest<FileModel>
{
    public IFormFile File { get; set; } = default!;
}