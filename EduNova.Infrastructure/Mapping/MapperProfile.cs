using AutoMapper;
using EduNova.Core.DTO.Course;
using EduNova.Core.DTO.Tag;
using EduNova.Core.DTO.Tenant;
using EduNova.Infrastructure.Entities.Courses;
using EduNova.Infrastructure.Entities.Tenancy;
using EduNova.Infrastructure.MultiTenancy;

namespace EduNova.Infrastructure.Mapping
{
    public class MapperProfile: Profile
    {

        public MapperProfile()
        {
            CreateMap<Tenant, ReadTenantDTO>();

            // Course mappings
            CreateMap<CreateCourseDTO, Course>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            CreateMap<UpdateCourseDTO, Course>();
            CreateMap<Course, ReadCourseDTO>()
                .ForMember(x => x.CourseTags, opt => opt.MapFrom(src => src.CourseTags.Select(x => x.Tag)));

            // Tag mappings
            CreateMap<CreateTagDTO, Tag>();
            CreateMap<Tag, ReadTagDTO>();
        }
    }
}
