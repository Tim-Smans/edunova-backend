using EduNova.Infrastructure.Entities.Courses;
using EduNova.Infrastructure.Entities.Tenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Infrastructure.Repositories.Interfaces
{
    public interface ICourseRepo : IGenericRepo<Course>
    {

        Task<IEnumerable<Course>> GetCoursesWithTags();
    }
}
