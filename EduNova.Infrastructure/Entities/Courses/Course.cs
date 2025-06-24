using EduNova.Infrastructure.Entities.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Infrastructure.Entities.Courses
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string TargetAudience { get; set; }
        public bool Published { get; set; }
        public bool Public { get; set; }
        public bool IsSequential { get; set; }

        public Guid TenantId { get; set; }

        public ICollection<CourseTag>? CourseTags { get; set; }
        public ICollection<Module>? Modules { get; set; }


    }
}
