using AutoMapper;
using EduNova.Core.DTO.Course;
using EduNova.Infrastructure.Entities.Courses;
using EduNova.Infrastructure.MultiTenancy;
using EduNova.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EduNova.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITenantProvider _tenantProvider;
        private readonly IUnitOfWork _unitOfWork;

        public CourseController(IMapper mapper, ITenantProvider tenantProvider, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _tenantProvider = tenantProvider;
            _unitOfWork = unitOfWork;
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadCourseDTO>> ReadCourseById(Guid id)
        {
            Course? course = await _unitOfWork.CourseRepo.GetByIdAsync(id);

            if (course == null)
            {
                return NotFound("Unable to find course with that id");
            }

            ReadCourseDTO courseDTO = _mapper.Map<ReadCourseDTO>(course);

            return Ok(courseDTO);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> CreateCourse(CreateCourseDTO courseDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Course course = _mapper.Map<Course>(courseDTO);

            course.TenantId = _tenantProvider.TenantId;

            await _unitOfWork.CourseRepo.AddAsync(course);
            await _unitOfWork.CourseRepo.SaveAsync();

            ReadCourseDTO readCourseDTO = _mapper.Map<ReadCourseDTO>(course);

            return CreatedAtAction(nameof(ReadCourseById), new { id = course.Id }, readCourseDTO);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{courseId}")]
        public async Task<IActionResult> UpdateCourse(Guid courseId, UpdateCourseDTO courseDTO)
        {
            Course? originalCourse = await _unitOfWork.CourseRepo.GetByIdAsync(courseId);

            if (originalCourse == null)
            {
                return NotFound("No model exists with this id");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(courseDTO, originalCourse);
            _unitOfWork.CourseRepo.Update(originalCourse);
            await _unitOfWork.CourseRepo.SaveAsync();

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{courseId}")]
        public async Task<IActionResult> DeleteCourse(Guid courseId)
        {
            Course? course = await _unitOfWork.CourseRepo.GetByIdAsync(courseId);

            if(course == null)
            {
                return NotFound("No course found with that course id");
            }

            _unitOfWork.CourseRepo.Delete(course);
            await _unitOfWork.CourseRepo.SaveAsync();

            return NoContent();
        }

        [HttpGet("test-error")]
        public IActionResult ThrowTestError()
        {
            throw new Exception("Test exception");
        }
    }
}
