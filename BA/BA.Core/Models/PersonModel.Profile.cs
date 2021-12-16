using AutoMapper;
using AutoMapper.EquivalencyExpression;
using BA.Core.Commands.Person;

namespace BA.Core.Models;

public class PersonModelProfile : Profile
{
    public PersonModelProfile()
    {
        CreateMap<CreateCommand, Domain.Entities.Person>()
            .EqualityComparison((src, dest) => src.Id == 0);

        CreateMap<Domain.Entities.Person, PersonModel>();
        CreateMap<Domain.Entities.Person, GetCommand>();

        CreateMap<TeamModel, GetCommand>();

        CreateMap<UpdateCommand, Domain.Entities.Team>()
            .EqualityComparison((src, dest) => src.Id == dest.Id);

        CreateMap<UpdateCommand, GetQuery>();
        CreateMap<GetCommand, GetQuery>();
        CreateMap<DeleteCommand, GetQuery>();
    }
}