using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Infrastructure.Entities.Courses
{
    public class CourseAssigned
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public string? UserId { get; set; }
        public Guid? CourseId { get; set; }
    }
}
