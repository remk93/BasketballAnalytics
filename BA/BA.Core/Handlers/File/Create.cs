using AutoMapper;
using AutoMapper.EntityFrameworkCore;
using BA.Core.Commands.File;
using BA.Core.Models;
using BA.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BA.Core.Handlers.File;

public class CreateHandler : IRequestHandler<CreateCommand, FileModel>
{
    private readonly IDbContextFactory<EntitiesContext> _contextFactory;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CreateHandler(
        IDbContextFactory<EntitiesContext> contextFactory,
        IMediator mediator,
        IMapper mapper)
    {
        _contextFactory = contextFactory;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<FileModel> Handle(CreateCommand command, CancellationToken cancellationToken)
    {
        using var context = _contextFactory.CreateDbContext();
        await context.BeginTransactionAsync();

        var entity = await context.Files
            .Persist(_mapper)
            .InsertOrUpdateAsync(command, cancellationToken);

        await context.CommitTransactionAsync();

        command.Id = entity.Id;

        return await _mediator.Send(_mapper.Map<GetCommand>(command), cancellationToken);
    }
}