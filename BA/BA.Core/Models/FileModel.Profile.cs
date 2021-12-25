using AutoMapper;
using AutoMapper.EquivalencyExpression;
using BA.Core.Commands.File;

namespace BA.Core.Models;

public class FileModelProfile : Profile
{
    public FileModelProfile()
    {
        CreateMap<CreateCommand, FileModel>();
        CreateMap<GetCommand, GetQuery>();

        CreateMap<FileModel, Domain.Entities.File>()
            .EqualityComparison((src, dest) => src.Id == dest.Id);

        CreateMap<Domain.Entities.File, FileModel>();
        CreateMap<Domain.Entities.File, GetCommand>();
    }
}