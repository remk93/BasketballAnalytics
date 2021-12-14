using BA.Core.Commands.Team;
using BA.Core.Endpoints;
using BA.Core.Models;
using MediatR;

namespace BA.Admin.Endpoints;

public class TeamEndpoints : BaseEndpoint, ITeamEndpoints
{
    public TeamEndpoints(IMediator mediator) : base(mediator)
    {
    }

    public async Task<IResult> Create(CreateCommand command)
    {
        return await HandleAsync<TeamModel, CreateCommand>(command);
    }

    public async Task<IResult> Get(GetCommand command)
    {
        return await HandleAsync<TeamModel, GetCommand>(command);
    }

    public async Task<IResult> Update(UpdateCommand command)
    {
        return await HandleAsync<TeamModel, UpdateCommand>(command);
    }

    public async Task<IResult> Delete(DeleteCommand command)
    {
        return await HandleAsync<Unit, DeleteCommand>(command);
    }
}