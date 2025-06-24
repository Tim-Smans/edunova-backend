using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Core.DTO.Course
{
    public class UpdateCourseDTO
    {
        public string Title { get; set; }
        [
            Required(ErrorMessage = "A course description is required"),
            MinLength(5, ErrorMessage = "A course description needs to be at least 5 characters long"),
        ]
        public string Description { get; set; }
        [
            Required(ErrorMessage = "A course requires a category")
        ]
        public string Category { get; set; }
        [
            Required(ErrorMessage = "A course requires a target audience")
        ]
        public string TargetAudience { get; set; }
        public bool Published { get; set; }
        public bool Public { get; set; }
        public bool IsSequential { get; set; }
    }
}
