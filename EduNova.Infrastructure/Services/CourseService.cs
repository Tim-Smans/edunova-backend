using AutoMapper;
using EduNova.Core.DTO.Course;
using EduNova.Core.DTO.Tag;
using EduNova.Infrastructure.Entities.Courses;
using EduNova.Infrastructure.MultiTenancy;
using EduNova.Infrastructure.Repositories.Interfaces;
using EduNova.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Infrastructure.Services
{
    public class CourseService : ICourseService
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantProvider _tenantProvider;

        public CourseService(IMapper mapper, IUnitOfWork unitOfWork, ITenantProvider tenantProvider)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _tenantProvider = tenantProvider;
        }

        public async Task<ReadCourseDTO> CreateCourse(CreateCourseDTO courseDTO)
        {
            Course course = _mapper.Map<Course>(courseDTO);

            course.TenantId = _tenantProvider.TenantId;
            course.Id = Guid.NewGuid();

            await _unitOfWork.CourseRepo.AddAsync(course);
            await _unitOfWork.CourseRepo.SaveAsync();

            ReadCourseDTO readCourseDTO = _mapper.Map<ReadCourseDTO>(course);

            return readCourseDTO;
        }

        public async Task DeleteCourse(Guid courseId)
        {
            Course? course = await _unitOfWork.CourseRepo.GetByIdAsync(courseId);

            if (course == null)
            {
                throw new KeyNotFoundException("No course found with that course id");
            }

            _unitOfWork.CourseRepo.Delete(course);
            await _unitOfWork.CourseRepo.SaveAsync();
        }

        public async Task<ReadCourseDTO> ReadCourseById(Guid id)
        {
            Course? course = await _unitOfWork.CourseRepo.GetByIdAsync(id);

            if (course == null)
            {
                throw new KeyNotFoundException("Unable to find course with that id");
            }

            ReadCourseDTO courseDTO = _mapper.Map<ReadCourseDTO>(course);

            return courseDTO;
        }

        public async Task UpdateCourse(Guid courseId, UpdateCourseDTO courseDTO)
        {
            Course? originalCourse = await _unitOfWork.CourseRepo.GetByIdAsync(courseId);

            if (originalCourse == null)
            {
                throw new KeyNotFoundException("No model exists with this id");
            }

            _mapper.Map(courseDTO, originalCourse);

            _unitOfWork.CourseRepo.Update(originalCourse);
            await _unitOfWork.CourseRepo.SaveAsync();
        }
    }
}
