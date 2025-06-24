using EduNova.Infrastructure.Entities.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Infrastructure.Entities.CourseModules
{
    public class ModulePowerpoint: Module
    {
        public string FileUrl { get; set; }
        public string FileName { get; set; }
        public int? SizeMb { get; set; }
        public int? SlideCount { get; set; }
        public string? ViewMode { get; set; }

    }
}
