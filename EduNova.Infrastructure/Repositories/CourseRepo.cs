using EduNova.Infrastructure.Entities.Courses;
using EduNova.Infrastructure.Entities.Tenancy;
using EduNova.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Infrastructure.Repositories
{
    public class CourseRepo : GenericRepo<Course>, ICourseRepo
    {
        public CourseRepo(NovaDBContext context) : base(context) { }


        public async Task<IEnumerable<Course>> GetCoursesWithTags()
        {
            return await _context.Courses
                .Include(x => x.CourseTags)
                .ToListAsync();
        }
    }
}
