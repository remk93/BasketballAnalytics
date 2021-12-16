using BA.Core.Models;
using MediatR;

namespace BA.Core.Commands.Person;

public class CreateCommand : PersonModel, IRequest<PersonModel>
{
}