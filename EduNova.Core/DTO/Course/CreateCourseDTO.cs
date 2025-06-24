using System.ComponentModel.DataAnnotations;

namespace EduNova.Core.DTO.Course
{
    public class CreateCourseDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [
            Required(ErrorMessage ="A course title is required"),
            MinLength(5, ErrorMessage ="A course title needs to be at least 5 characters long"),
        ]
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
        public bool Published { get; set; } = false;
        public bool Public { get; set; } = false;
        public bool IsSequential { get; set; } = true;
    }
}
