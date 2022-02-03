using AutoMapper;
using AutoMapper.EquivalencyExpression;
using BA.Core.Commands.File;
using BA.Core.Handlers.File.Queries;

namespace BA.Core.Models;

public class FileModelProfile : Profile
{
    public FileModelProfile()
    {
        CreateMap<DownloadCommand, FileModel>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.File.FileName))
            .ForMember(dest => dest.Link, opt => opt.MapFrom(src => 
                $"{Guid.NewGuid():N}{Path.GetExtension(src.File.FileName)}"));

        CreateMap<FileModel, CreateCommand>();

        CreateMap<CreateCommand, Domain.Entities.File>()
            .EqualityComparison((src, dest) => src.Id == dest.Id);

        CreateMap<CreateCommand, GetCommand>();
        CreateMap<GetCommand, GetQuery>();

        CreateMap<Domain.Entities.File, FileModel>();
    }
}