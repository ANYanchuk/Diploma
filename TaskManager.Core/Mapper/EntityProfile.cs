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
            CreateMap<ReportFormat, string>().ConvertUsing((format, str) => format.Name); ;
            CreateMap<Errand, ErrandEntity>().ReverseMap();
            CreateMap<ErrandEntity, Errand>()
                .ForMember(ee => ee.ReportFormat, opt => opt.Ignore())
                .ForMember(ee => ee.ReportFormatName, opt => opt
                    .MapFrom(src => src.ReportFormat)).ReverseMap();
            CreateMap<UserEntity, ApplicationUser>()
                .ForMember(au => au.Role, opt => opt.Ignore())
                .ForMember(au => au.RoleName, opt => opt
                    .MapFrom(src => src.Role)).ReverseMap();
            CreateMap<UploadedFile, FileEntity>().ReverseMap();
            CreateMap<Report, ReportEntity>().ReverseMap();
        }
    }
}