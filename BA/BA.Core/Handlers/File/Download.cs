using AutoMapper;
using BA.Core.Commands.File;
using BA.Core.Exceptions;
using BA.Core.Models;
using BA.Core.Options;
using BA.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BA.Core.Handlers.File;

public class DownloadHandler : IRequestHandler<DownloadCommand, FileModel>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly FileStorageOptions _fileStorageOptions;

    public DownloadHandler(
        IMapper mapper,
        IMediator mediator,
        IOptions<FileStorageOptions> fileStorageOptions)
    {
        _mapper = mapper;
        _mediator = mediator;
        _fileStorageOptions = fileStorageOptions.Value;
    }

    public async Task<FileModel> Handle(DownloadCommand command, CancellationToken cancellationToken)
    {
        var model = _mapper.Map<FileModel>(command);

        if (!_fileStorageOptions.AllowedExtensions.Any(a => model.Link.EndsWith(a)))
            throw new BadRequestException($"Extention of '{model.Name}' is not allowed to save");

        using (var stream = System.IO.File.Create(Path.Combine(_fileStorageOptions.DownloadsFolder, model.Link)))
        {
            await command.File.CopyToAsync(stream, cancellationToken);
        }

        return await _mediator.Send(_mapper.Map<CreateCommand>(model), cancellationToken);
    }
}