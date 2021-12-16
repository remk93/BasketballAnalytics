using BA.Core.Models;
using MediatR;

namespace BA.Core.Commands.Person;

public class UpdateCommand : PersonModel, IRequest<PersonModel>
{
}