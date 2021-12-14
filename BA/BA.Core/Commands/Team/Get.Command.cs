using BA.Core.Models;
using MediatR;

namespace BA.Core.Commands.Team;

public class GetCommand : IRequest<TeamModel>
{
    public int Id { get; set; }
}