using AutoMapper;
using AutoMapper.EquivalencyExpression;
using BA.Core.Commands.Team;
using BA.Core.Handlers.Team.Queries;

namespace BA.Core.Models;

public class TeamModelProfile : Profile
{
    public TeamModelProfile()
    {
        CreateMap<CreateCommand, Domain.Entities.Team>()
            .EqualityComparison((src, dest) => src.Id == 0)
            .ForMember(dest => dest.Logo, opt => opt.Ignore())
            .ForMember(dest => dest.LogoId, opt => opt.MapFrom(src => src.Id));

        CreateMap<UpdateCommand, Domain.Entities.Team>()
            .EqualityComparison((src, dest) => src.Id == dest.Id)
            .ForMember(dest => dest.Logo, opt => opt.Ignore())
            .ForMember(dest => dest.LogoId, opt => opt.MapFrom(src => src.Id));

        CreateMap<UpdateCommand, GetQuery>();
        CreateMap<GetCommand, GetQuery>();
        CreateMap<DeleteCommand, GetQuery>();

        CreateMap<Domain.Entities.Team, GetCommand>();
        CreateMap<Domain.Entities.Team, TeamModel>();
    }
}