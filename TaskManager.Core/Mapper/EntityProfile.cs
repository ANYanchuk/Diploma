using AutoMapper;

using TaskManager.Core.Models.Data;
using TaskManager.Core.Models.Entities;

namespace TaskManager.Core.Mapper
{
    public class EntityProfile : Profile
    {
        public EntityProfile()
        {
            CreateMap<ApplicationRole, string>().ConvertUsing((role, str) => role.Name);
            CreateMap<ReportFormat, string>().ConvertUsing((format, str) => format.Name);;
            CreateMap<Errand, ErrandEntity>().ReverseMap();
            CreateMap<UserEntity, ApplicationUser>()
                .ForMember(au => au.Role, opt => opt.Ignore())
                .ForMember(au => au.RoleName, opt => opt
                    .MapFrom(src => src.Role)).ReverseMap();
            CreateMap<UploadedFile, FileEntity>().ReverseMap();
            CreateMap<Report, ReportEntity>().ReverseMap();
        }
    }
}