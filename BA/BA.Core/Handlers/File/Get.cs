using AutoMapper;
using AutoMapper.QueryableExtensions;
using BA.Core.Commands.File;
using BA.Core.Exceptions;
using BA.Core.Handlers.File.Queries;
using BA.Core.Models;
using BA.Core.Queries;
using BA.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BA.Core.Handlers.File;

public class GetHandler : IRequestHandler<GetCommand, FileModel>
{
    private readonly IDbContextFactory<EntitiesContext> _contextFactory;
    private readonly IMapper _mapper;

    public GetHandler(
        IDbContextFactory<EntitiesContext> contextFactory,
        IMapper mapper)
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
    }

    public async Task<FileModel> Handle(GetCommand command, CancellationToken cancellationToken)
    {
        using var context = _contextFactory.CreateDbContext();

        return await context.Files
            .ByQuery(_mapper.Map<GetQuery>(command))
            .ProjectTo<FileModel>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken) ??
                throw new NotFoundException($"File/{command.Id} was not found");
    }
}