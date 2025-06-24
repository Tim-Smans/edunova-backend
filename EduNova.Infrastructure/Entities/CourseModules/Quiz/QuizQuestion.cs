using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Infrastructure.Entities.CourseModules.Quiz
{
    public class QuizQuestion
    {
        public Guid Id { get; set; }
        public Guid? QuizId { get; set; }
        public Guid? TenantId { get; set; }
        public string Question { get; set; }
        public string CorrectAnswer { get; set; }
        public ICollection<string> Options { get; set; }
    }
}
