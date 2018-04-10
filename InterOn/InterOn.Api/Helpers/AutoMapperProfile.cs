using System.Linq;
using AutoMapper;
using InterOn.Data.DbModels;
using InterOn.Data.ModelsDto;
using InterOn.Data.ModelsDto.Category;
using InterOn.Data.ModelsDto.Group;

namespace InterOn.Api.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //CreateMap<User, UserDto>();
            //CreateMap<UserDto, User>();

            CreateMap<Role, RoleDto>();
            CreateMap<RoleDto, Role>();

            CreateMap<UserRoleDto, UserRole>();
            CreateMap<UserRole, UserRoleDto>();

            CreateMap<UserToken, UserTokenDto>();
            CreateMap<UserTokenDto, UserToken>();

            CreateMap<CreateUserDto, User>();
            CreateMap<User, CreateUserDto>();

            CreateMap<LoginUserDto, User>()
                .ForMember(gdt => gdt.Id, opt => opt.MapFrom(g => g.UserId));

            //Group
            CreateMap<Group, GroupDto>()
                .ForMember(gdt => gdt.SubCategories,
                    otp => otp.MapFrom(g => g.SubCategories.Select(id =>
                        new GroupSubCategoryDto {Id = id.SubCategoryId, Name = id.SubCategory.Name})));
              
            CreateMap<Group, CreateGroupDto>()
                .ForMember(gdt => gdt.SubCategories,
                    opt => opt.MapFrom(g => g.SubCategories.Select(gd => gd.SubCategoryId)));
            CreateMap<CreateGroupDto, Group>()
                .ForMember(g => g.SubCategories,
                    opt => opt.MapFrom(gdt => gdt.SubCategories.Select(id => new GroupCategory { SubCategoryId = id })));


            CreateMap<Group, UpdateGroupDto>()
                .ForMember(gdt => gdt.SubCategories,
                    opt => opt.MapFrom(g => g.SubCategories.Select(gd => gd.SubCategoryId)));
            CreateMap<UpdateGroupDto, Group>()
                .ForMember(g => g.Id, opt => opt.Ignore())
                .ForMember(g => g.SubCategories,
                    opt => opt.MapFrom(gdt => gdt.SubCategories.Select(id => new GroupCategory {SubCategoryId = id})))
                .ForMember(g => g.SubCategories, opt => opt.Ignore())
                .AfterMap((gdto, g) =>
                {
                    //Remove
                    var removeCategories = g.SubCategories.Where(s => !gdto.SubCategories.Contains(s.SubCategoryId))
                        .ToList();
                    foreach (var s in removeCategories.ToList())
                         g.SubCategories.Remove(s);
                  //add
                    var addedCategories = gdto.SubCategories
                        .Where(id => g.SubCategories.All(s => s.SubCategoryId != id))
                        .Select(id => new GroupCategory { SubCategoryId = id })
                        .ToList();
                    foreach (var c in addedCategories.ToList())
                        g.SubCategories.Add(c);
                });

            //MainCategory
            CreateMap<SaveCategoryDto, MainCategory>()
                .ForMember(g => g.Id, opt => opt.Ignore());

            CreateMap<MainCategory, SaveCategoryDto>();
            
            //SubCategory
            CreateMap<SubCategoryDto, SubCategory>();
            CreateMap<SubCategory, SubCategoryDto>();
           
            CreateMap<SaveCategoryDto, SubCategory>()
                .ForMember(g => g.Id, opt => opt.Ignore());

            //photo

            CreateMap<GroupPhoto, GroupPhotoDto>();

        }
    }
}
