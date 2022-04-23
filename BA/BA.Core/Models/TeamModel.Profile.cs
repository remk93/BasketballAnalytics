using AutoMapper;
using AutoMapper.EquivalencyExpression;
using BA.Core.Handlers.Team.Commands;
using BA.Core.Handlers.Team.Queries;

namespace BA.Core.Models;

public class TeamModelProfile : Profile
{
    public TeamModelProfile()
    {
        CreateMap<CreateCommand, Domain.Entities.Team>()
            .EqualityComparison((src, dest) => dest.Id == 0)
            .EqualityComparison((src, dest) => src.Id == dest.Id)
            .ForMember(dest => dest.Logo, opt => opt.Ignore())
            .ForMember(dest => dest.LogoId, opt => opt.MapFrom(src => src.Logo.Id));

        CreateMap<CreateCommand, GetCommand>();

        CreateMap<UpdateCommand, Domain.Entities.Team>()
            .EqualityComparison((src, dest) => src.Id == dest.Id)
            .ForMember(dest => dest.Logo, opt => opt.Ignore())
            .ForMember(dest => dest.LogoId, opt => opt.MapFrom(src => src.Logo.Id));

        CreateMap<UpdateCommand, GetCommand>();
        CreateMap<UpdateCommand, GetQuery>();

        CreateMap<GetCommand, GetQuery>();

        CreateMap<DeleteCommand, GetCommand>();

        CreateMap<TeamModel, Domain.Entities.Team>();
        CreateMap<Domain.Entities.Team, TeamModel>();

        CreateMap<GetAllCommand, GetAllQuery>();
    }
}