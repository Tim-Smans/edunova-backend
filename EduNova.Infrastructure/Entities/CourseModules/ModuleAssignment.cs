using EduNova.Infrastructure.Entities.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Infrastructure.Entities.CourseModules
{
    public class ModuleAssignment: Module
    {
        public string Description { get; set; }
        public int MaxFileSizeMb { get; set; }

    }
}
