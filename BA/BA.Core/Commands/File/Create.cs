using AutoMapper;
using AutoMapper.EntityFrameworkCore;
using BA.Core.Models;
using BA.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BA.Core.Commands.File;

internal class CreateHandler : IRequestHandler<CreateCommand, FileModel>
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

            var entity = await context.Files
                .Persist(_mapper)
                .InsertOrUpdateAsync(_mapper.Map<FileModel>(command), cancellationToken);

            await context.SaveChangesAsync(cancellationToken);

            return await _mediator.Send(_mapper.Map<GetCommand>(entity), cancellationToken);
        }
    }