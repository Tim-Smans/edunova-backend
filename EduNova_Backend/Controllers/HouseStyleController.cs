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
    public class HouseStyleController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HouseStyleController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ReadHousestyles()
        {
            ICollection<HouseStyle> houseStyles = await _unitOfWork.HouseStyleRepo.GetAllAsync();
            if (houseStyles.IsNullOrEmpty())
            {
                return NotFound("No housestyles found for your current tenant");
            }

            

            return Ok(houseStyles);
        }
    }
}
