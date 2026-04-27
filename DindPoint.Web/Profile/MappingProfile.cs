using AutoMapper;
using DindPoint.Application.DTOs.Auth;
using DindPoint.Web.ViewModels;
using DindPoint.Application.DTOs.Role;
using DindPoint.Domain.Entities;
using DindPoint.Application.DTOs.Department;

namespace DindPoint.Web.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<LoginViewModel, LoginDto>();

        CreateMap<RoleCreateDto, Role>();
        CreateMap<RoleUpdateDto, Role>();
        CreateMap<Role, RoleDto>();
        CreateMap<RoleUpdateDto, Role>();
        CreateMap<RoleDto, RoleUpdateDto>();

        CreateMap<DepartmentCreateDto, Department>();
        CreateMap<DepartmentUpdateDto, Department>();
        CreateMap<Department, DepartmentDto>();
        CreateMap<DepartmentUpdateDto, Department>();
        CreateMap<DepartmentDto, DepartmentUpdateDto>();
    }
}