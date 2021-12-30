using AutoMapper;
using BA.Core.Exceptions;
using BA.Core.Models;
using BA.Core.Options;
using BA.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BA.Core.Commands.File;

public class DownloadHandler : IRequestHandler<DownloadCommand, FileModel>
{
    private readonly IDbContextFactory<EntitiesContext> _contextFactory;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly FileStorageOptions _fileStorageOptions;

    public DownloadHandler(
        IDbContextFactory<EntitiesContext> contextFactory,
        IMapper mapper,
        IMediator mediator,
        IOptions<FileStorageOptions> fileStorageOptions)
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
        _mediator = mediator;
        _fileStorageOptions = fileStorageOptions.Value;
    }

    public async Task<FileModel> Handle(DownloadCommand command, CancellationToken cancellationToken)
    {
        using var context = _contextFactory.CreateDbContext();

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