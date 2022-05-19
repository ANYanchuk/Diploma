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
            CreateMap<RegisterViewModel, UserEntity >().ReverseMap();
            CreateMap<UserViewModel, UserEntity>().ReverseMap();
            CreateMap<UserIdViewModel, UserEntity>().ReverseMap();
            CreateMap<PostErrandViewModel, ErrandEntity>().ReverseMap();
        }
    }
}
