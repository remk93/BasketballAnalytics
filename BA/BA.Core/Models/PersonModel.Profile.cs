using AutoMapper;
using AutoMapper.EquivalencyExpression;
using BA.Core.Commands.Person;

namespace BA.Core.Models;

public class PersonModelProfile : Profile
{
    public PersonModelProfile()
    {
        CreateMap<CreateCommand, PersonModel>();
        CreateMap<UpdateCommand, PersonModel>();

        CreateMap<UpdateCommand, GetQuery>();
        CreateMap<GetCommand, GetQuery>();
        CreateMap<DeleteCommand, GetQuery>();

        CreateMap<Domain.Entities.Person, GetCommand>();
        CreateMap<Domain.Entities.Person, PersonModel>();

        CreateMap<PersonModel, GetCommand>();
        CreateMap<PersonModel, Domain.Entities.Person>()
            .EqualityComparison((src, dest) => src.Id == dest.Id);
    }
}