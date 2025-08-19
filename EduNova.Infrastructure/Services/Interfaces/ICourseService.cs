using EduNova.Core.DTO.Course;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Infrastructure.Services.Interfaces
{
    public interface ICourseService
    {
        Task<ReadCourseDTO> ReadCourseById(Guid id);
        Task<IEnumerable<ReadCourseDTO>> ReadCourses();
        Task<ReadCourseDTO> CreateCourse(CreateCourseDTO courseDTO);
        Task UpdateCourse(Guid courseId, UpdateCourseDTO courseDTO);
        Task DeleteCourse(Guid courseId);

    }
}
