using MediatR;

namespace BA.Core.Commands.Team;

public class DeleteCommand : IRequest<Unit>
{
    public int Id { get; set; }
}