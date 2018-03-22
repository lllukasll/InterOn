using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InterOn.Data.DbModels;
using InterOn.Data.ModelsDto;

namespace InterOn.Api.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<Role, RoleDto>();
            CreateMap<RoleDto, Role>();

            CreateMap<UserRoleDto, UserRole>();
            CreateMap<UserRole, UserRoleDto>();

            CreateMap<UserToken, UserTokenDto>();
            CreateMap<UserTokenDto, UserToken>();

            CreateMap<GroupDto, Group>()
                .ForMember(g => g.SubCategories,
                    opt => opt.MapFrom(gdt => gdt.SubCategory.Select(id => new GroupCategory {SubCategoryId = id})));

            CreateMap<Group, GroupDto>()
                .ForMember(gdt => gdt.SubCategory,
                    opt => opt.MapFrom(g => g.SubCategories.Select(gd => gd.SubCategoryId)));
        }
    }
}
