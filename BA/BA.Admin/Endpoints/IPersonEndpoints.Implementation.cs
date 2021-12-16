using BA.Core.Commands.Person;
using BA.Core.Endpoints;
using BA.Core.Models;
using MediatR;

namespace BA.Admin.Endpoints;

public class PersonEndpoints : BaseEndpoint, IPersonEndpoints
{
    public PersonEndpoints(IMediator mediator) : base(mediator)
    {
    }

    public async Task<IResult> Create(CreateCommand command)
    {
        return await HandleAsync<PersonModel, CreateCommand>(command);
    }

    public async Task<IResult> Get(GetCommand command)
    {
        return await HandleAsync<PersonModel, GetCommand>(command);
    }

    public async Task<IResult> Update(UpdateCommand command)
    {
        return await HandleAsync<PersonModel, UpdateCommand>(command);
    }

    public async Task<IResult> Delete(DeleteCommand command)
    {
        return await HandleAsync<Unit, DeleteCommand>(command);
    }
}