using MediatR;

namespace BA.Core.Commands.Person;

public class DeleteCommand : IRequest<Unit>
{
    public int Id { get; set; }
}