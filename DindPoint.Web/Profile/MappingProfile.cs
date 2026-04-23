using AutoMapper;
using DindPoint.Application.DTOs.Auth;
using DindPoint.Web.ViewModels;

namespace DindPoint.Web.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<LoginViewModel, LoginDto>();
    }
}