using EduNova.Infrastructure.Entities.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Infrastructure.Entities.CourseModules.Quiz
{
    public class ModuleQuiz: Module
    {
        public int PassingScore { get; set; }

        public ICollection<QuizQuestion>? Questions { get; set; }

    }
}
