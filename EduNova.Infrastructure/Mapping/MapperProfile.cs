using AutoMapper;
using EduNova.Core.DTO.Course;
using EduNova.Core.DTO.Tenant;
using EduNova.Infrastructure.Entities.Courses;
using EduNova.Infrastructure.Entities.Tenancy;

namespace EduNova.Infrastructure.Mapping
{
    public class MapperProfile: Profile
    {

        public MapperProfile()
        {
            CreateMap<Tenant, ReadTenantDTO>();
            CreateMap<CreateCourseDTO, Course>();
            CreateMap<UpdateCourseDTO, Course>();
            CreateMap<Course, ReadCourseDTO>();
        }
    }
}
