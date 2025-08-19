using AutoMapper;
using EduNova.Core.DTO.Tenant;
using EduNova.Infrastructure.Entities.Tenancy;
using EduNova.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EduNova.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TenantController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ReadTenantById(Guid id)
        {
            Tenant? tenant = await _unitOfWork.TenantRepo.GetByIdAsync(id);
            if (tenant == null)
            {
                return NotFound("No tenant found with this id");
            }

            ReadTenantDTO readTenantDTO = _mapper.Map<ReadTenantDTO>(tenant);

            return Ok(readTenantDTO);
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ReadTenants()
        {
            IEnumerable<Tenant>? tenants = await _unitOfWork.TenantRepo.GetAllAsync();
            if (tenants.IsNullOrEmpty())
            {
                return NotFound("No tenants found");
            }

            IEnumerable<ReadTenantDTO> readTenantDTOs = _mapper.Map<IEnumerable<ReadTenantDTO>>(tenants);

            return Ok(readTenantDTOs);
        }

        [AllowAnonymous]
        [HttpGet("by-name/{name}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ReadTenantByName(string name)
        {
            Tenant? tenant = await _unitOfWork.TenantRepo.GetByNameAsync(name);
            if (tenant == null)
            {
                return NotFound("No tenant found with this name");
            }

            ReadTenantDTO readTenantDTO = _mapper.Map<ReadTenantDTO>(tenant);

            return Ok(readTenantDTO);
        }
    }
}
