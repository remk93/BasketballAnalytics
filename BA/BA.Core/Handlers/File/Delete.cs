using AutoMapper;
using AutoMapper.EntityFrameworkCore;
using BA.Core.Commands.File;
using BA.Core.Options;
using BA.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BA.Core.Handlers.File;

public class DeleteHandler : IRequestHandler<DeleteCommand, Unit>
{
    private readonly IDbContextFactory<EntitiesContext> _contextFactory;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly FileStorageOptions _fileStorageOptions;

    public DeleteHandler(
        IDbContextFactory<EntitiesContext> contextFactory,
        IMediator mediator,
        IMapper mapper,
        IOptions<FileStorageOptions> fileStorageOptions)
    {
        _contextFactory = contextFactory;
        _mediator = mediator;
        _mapper = mapper;
        _fileStorageOptions = fileStorageOptions.Value;
    }

    public async Task<Unit> Handle(DeleteCommand command, CancellationToken cancellationToken)
    {
        using var context = _contextFactory.CreateDbContext();

        var model = await _mediator.Send(_mapper.Map<GetCommand>(command), cancellationToken);

        await context.BeginTransactionAsync();

        await context.Files
            .Persist(_mapper)
            .RemoveAsync(model, cancellationToken);

        await context.CommitTransactionAsync();

        RemoveFile(command.Link);

        return Unit.Value;
    }

    private void RemoveFile(string link)
    {
        var path = Path.Combine(Path.Combine(_fileStorageOptions.DownloadsFolder, link));

        if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
    }
}