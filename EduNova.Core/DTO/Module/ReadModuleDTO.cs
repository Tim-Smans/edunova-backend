using EduNova.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Core.DTO.Module
{
    public class ReadModuleDTO
    {
        public Guid Id { get; set; }
        public Guid? CourseId { get; set; }
        public ModuleType Type { get; set; }
        public string Title { get; set; }
        public int Sequence { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
