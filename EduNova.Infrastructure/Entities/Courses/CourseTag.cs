using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Infrastructure.Entities.Courses
{
    public class CourseTag
    {

        public Guid Id { get; set; }
        public Guid? CourseId { get; set; }
        public Guid? TagId { get; set; }
        public Guid TenantId { get; set; }

        public Tag? Tag { get; set; }
        public Course? Course { get; set; }
    }
}
