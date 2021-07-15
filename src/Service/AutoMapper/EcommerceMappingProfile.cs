using AutoMapper;
using Domain.Entities;
using Domain.ViewModels;
using Domain.ViewModels.Image;
using Domain.ViewModels.User;
using System;

namespace Service.AutoMapper
{
    public class EcommerceMappingProfile : Profile
    {
        public EcommerceMappingProfile()
        {
            CreateMap<UserSignUpViewModel, User>();
            CreateMap<User, UserOutputViewModel>();
            CreateMap<UserLogInViewModel, User>();
            CreateMap<User, UserTokenViewModel>();
            CreateMap<ImageViewModel, Image>()
                .ForMember(i => i.Content, map =>
                {
                    map.MapFrom(ivm => Convert.FromBase64String(ivm.Content));
                });
            CreateMap<Image, ImageViewModel>()
                .ForMember(ivm => ivm.Content, map =>
                {
                    map.MapFrom(i => Convert.ToBase64String(i.Content));
                });
            CreateMap<UserUpdateViewModel, User>();
        }
    }
}
