using AutoMapper;

using TaskManager.ViewModels;
using TaskManager.Core.Models.Entities;

namespace TaskManager.Mapper
{
    public class ViewModelProfile : Profile
    {
        public ViewModelProfile()
        {
            CreateMap<ErrandViewModel, ErrandEntity>().ReverseMap();
            CreateMap<RegisterViewModel, UserEntity>().ReverseMap();
            CreateMap<UserViewModel, UserEntity>().ReverseMap();
            CreateMap<UserIdViewModel, UserEntity>().ReverseMap();
            CreateMap<PostErrandViewModel, ErrandEntity>().ReverseMap();
            CreateMap<IFormFile, byte[]>().ConvertUsing<IFormFileTypeConverter>();
            CreateMap<ReportViewModel, ReportEntity>()
                .ForMember(m => m.Files, opt => opt.Ignore()).ReverseMap();
        }

        public class IFormFileTypeConverter : ITypeConverter<IFormFile, byte[]>
        {
            public byte[] Convert(IFormFile source, byte[] bytes, ResolutionContext ctx)
            {
                using (var ms = new MemoryStream())
                {
                    source.CopyTo(ms);
                    return ms.ToArray();
                }
            }
        }
    }
}
