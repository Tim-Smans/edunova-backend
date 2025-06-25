using AutoMapper;
using EduNova.Core.DTO.Course;
using EduNova.Infrastructure.Entities.Courses;
using EduNova.Infrastructure.MultiTenancy;
using EduNova.Infrastructure.Repositories.Interfaces;
using EduNova.Infrastructure.Services.Interfaces;
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
         private readonly IUnitOfWork _unitOfWork;
        private readonly ICourseService _courseService;

        public CourseController(IMapper mapper, IUnitOfWork unitOfWork, ICourseService courseService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _courseService = courseService;
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadCourseDTO>> ReadCourseById(Guid id)
        {
            ReadCourseDTO courseDTO = await _courseService.ReadCourseById(id);

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

            ReadCourseDTO readCourseDTO = await _courseService.CreateCourse(courseDTO);

            return CreatedAtAction(nameof(ReadCourseById), new { id = readCourseDTO.Id }, readCourseDTO);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{courseId}")]
        public async Task<IActionResult> UpdateCourse(Guid courseId, UpdateCourseDTO courseDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _courseService.UpdateCourse(courseId, courseDTO);
                
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{courseId}")]
        public async Task<IActionResult> DeleteCourse(Guid courseId)
        {
            await _courseService.DeleteCourse(courseId);

            return NoContent();
        }
    }
}
