using AutoMapper;
using AutoMapper.QueryableExtensions;
using BA.Core.Exceptions;
using BA.Core.Models;
using BA.Core.Queries;
using BA.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BA.Core.Commands.Person;

public class GetHandler : IRequestHandler<GetCommand, PersonModel>
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

    public async Task<PersonModel> Handle(GetCommand command, CancellationToken cancellationToken)
    {
        using var context = _contextFactory.CreateDbContext();

        return await context.People
            .ByQuery(_mapper.Map<GetQuery>(command))
            .ProjectTo<PersonModel>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken) ??
                throw new NotFoundException($"Person/{command.Id} was not found");
    }
}