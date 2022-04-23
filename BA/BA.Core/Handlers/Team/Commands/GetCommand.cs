using BA.Core.Models;
using MediatR;

namespace BA.Core.Handlers.Team.Commands;

public class GetCommand : IRequest<TeamModel>
{
    public int Id { get; set; }
}