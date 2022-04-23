using MediatR;

namespace BA.Core.Handlers.Team.Commands;

public class DeleteCommand : IRequest<Unit>
{
    public int Id { get; set; }
}