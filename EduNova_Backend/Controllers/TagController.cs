using AutoMapper;
using EduNova.Core.DTO.Course;
using EduNova.Core.DTO.Tag;
using EduNova.Infrastructure.Entities.Courses;
using EduNova.Infrastructure.Repositories.Interfaces;
using EduNova.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduNova.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TagController: ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadTagDTO>> ReadTagById(Guid id)
        {
            ReadTagDTO tagDTO = await _tagService.ReadTagById(id);

            return Ok(tagDTO);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> CreateTag(CreateTagDTO tagDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ReadTagDTO readTagDTO = await _tagService.CreateTag(tagDTO);

            return CreatedAtAction(nameof(ReadTagById), new { id = readTagDTO.Id }, readTagDTO);
        }
    }
}
