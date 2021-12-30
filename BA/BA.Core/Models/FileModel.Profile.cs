using AutoMapper;
using AutoMapper.EquivalencyExpression;
using BA.Core.Commands.File;

namespace BA.Core.Models;

public class FileModelProfile : Profile
{
    public FileModelProfile()
    {
        CreateMap<DownloadCommand, FileModel>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.File.FileName))
            .ForMember(dest => dest.Link, opt => opt.MapFrom(src => 
                $"{Guid.NewGuid():N}{Path.GetExtension(src.File.FileName)}"));


        CreateMap<CreateCommand, FileModel>();
        CreateMap<GetCommand, GetQuery>();

        CreateMap<FileModel, Domain.Entities.File>()
            .EqualityComparison((src, dest) => src.Id == dest.Id);

        CreateMap<Domain.Entities.File, FileModel>();
        CreateMap<Domain.Entities.File, GetCommand>();
    }
}