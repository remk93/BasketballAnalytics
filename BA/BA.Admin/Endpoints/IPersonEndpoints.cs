using BA.Core.Commands.Person;

namespace BA.Admin.Endpoints;

public interface IPersonEndpoints
{
    Task<IResult> Create(CreateCommand command);
    Task<IResult> Get(GetCommand command);
    Task<IResult> Update(UpdateCommand command);
    Task<IResult> Delete(DeleteCommand command);
}