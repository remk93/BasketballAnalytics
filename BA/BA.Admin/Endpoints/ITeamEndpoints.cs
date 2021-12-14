using BA.Core.Commands.Team;

namespace BA.Admin.Endpoints;

public interface ITeamEndpoints
{
    Task<IResult> Create(CreateCommand command);
    Task<IResult> Get(GetCommand command);
    Task<IResult> Update(UpdateCommand command);
    Task<IResult> Delete(DeleteCommand command);
}