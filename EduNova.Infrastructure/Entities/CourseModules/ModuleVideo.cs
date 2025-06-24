using EduNova.Infrastructure.Entities.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Infrastructure.Entities.CourseModules
{
    public class ModuleVideo: Module
    {
        public string VideoUrl { get; set; }
        public int? Duration { get; set; }

    }
}
