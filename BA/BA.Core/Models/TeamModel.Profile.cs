using AutoMapper;
using AutoMapper.EquivalencyExpression;
using BA.Core.Commands.Team;

namespace BA.Core.Models;

public class TeamModelProfile : Profile
{
    public TeamModelProfile()
    {
        CreateMap<CreateCommand, Domain.Entities.Team>()
            .EqualityComparison((src, dest) => src.Id == 0);

        CreateMap<Domain.Entities.Team, TeamModel>();
        CreateMap<Domain.Entities.Team, GetCommand>();

        CreateMap<TeamModel, GetCommand>();

        CreateMap<UpdateCommand, Domain.Entities.Team>()
            .EqualityComparison((src, dest) => src.Id == dest.Id);

        CreateMap<UpdateCommand, GetQuery>();
        CreateMap<GetCommand, GetQuery>();
        CreateMap<DeleteCommand, GetQuery>();
    }
}