using BA.Core.Models;
using MediatR;

namespace BA.Core.Handlers.Team.Commands;

public class UpdateCommand : TeamModel, IRequest<TeamModel>
{
}