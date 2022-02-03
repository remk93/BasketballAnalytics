using BA.Core.Models;
using MediatR;

namespace BA.Core.Commands.Team;

public class UpdateCommand : TeamModel, IRequest<TeamModel>
{
}