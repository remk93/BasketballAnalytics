using BA.Core.Models;
using MediatR;

namespace BA.Core.Commands.Person;

public class GetCommand : IRequest<PersonModel>
{
    public int Id { get; set; }
}