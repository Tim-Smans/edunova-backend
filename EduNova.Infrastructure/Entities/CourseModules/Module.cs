using EduNova.Infrastructure.Entities.CourseModules;
using EduNova.Infrastructure.Entities.CourseModules.Quiz;
using EduNova.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Infrastructure.Entities.Modules
{
    public abstract class Module
    {

        public Guid Id { get; set; }
        public Guid? CourseId { get; set; }
        public Guid TenantId { get; set; }
        public ModuleType Type { get; set; }
        public string Title { get; set; }
        public int Sequence { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
