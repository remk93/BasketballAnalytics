using AutoMapper;
using BA.Core.Models;
using BA.Core.Options;
using BA.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BA.Core.Commands.File;

public class DownloadHandler : IRequestHandler<DownloadCommand, FileModel>
{
    private readonly IDbContextFactory<EntitiesContext> _contextFactory;
    private readonly IMediator _mediator;
    private readonly FileStorageOptions _fileStorageOptions;

    public DownloadHandler(
        IDbContextFactory<EntitiesContext> contextFactory,
        IMediator mediator,
        IOptions<FileStorageOptions> fileStorageOptions)
    {
        _contextFactory = contextFactory;
        _mediator = mediator;
        _fileStorageOptions = fileStorageOptions.Value;
    }

    public async Task<FileModel> Handle(DownloadCommand command, CancellationToken cancellationToken)
    {
        using var context = _contextFactory.CreateDbContext();

        var link = $"{Guid.NewGuid():N}{Path.GetExtension(command.File.FileName)}";

        using (var stream = System.IO.File.Create(Path.Combine(_fileStorageOptions.DownloadsFolder, link)))
        {
            await command.File.CopyToAsync(stream, cancellationToken);
        }

        return await _mediator.Send(new CreateCommand
        {
            Link = link,
            Name = command.File.FileName
        }, cancellationToken);
    }
}