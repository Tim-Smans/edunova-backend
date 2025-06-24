using EduNova.Core.DTO.Module;
using EduNova.Core.DTO.Tag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Core.DTO.Course
{
    public class ReadCourseDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string TargetAudience { get; set; }
        public bool Published { get; set; }
        public bool Public { get; set; }
        public bool IsSequential { get; set; }

        public ICollection<ReadTagDTO> CourseTags { get; set; } = [];
        public ICollection<ReadModuleDTO> Modules { get; set; } = [];
    }
}
