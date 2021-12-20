using AutoMapper;
using AutoMapper.EquivalencyExpression;
using BA.Core.Commands.Team;

namespace BA.Core.Models;

public class TeamModelProfile : Profile
{
    public TeamModelProfile()
    {
        CreateMap<CreateCommand, TeamModel>();
        CreateMap<UpdateCommand, TeamModel>();

        CreateMap<UpdateCommand, GetQuery>();
        CreateMap<GetCommand, GetQuery>();
        CreateMap<DeleteCommand, GetQuery>();

        CreateMap<Domain.Entities.Team, GetCommand>();
        CreateMap<Domain.Entities.Team, TeamModel>();

        CreateMap<TeamModel, GetCommand>();
        CreateMap<TeamModel, Domain.Entities.Team>()
            .EqualityComparison((src, dest) => src.Id == dest.Id);
    }
}