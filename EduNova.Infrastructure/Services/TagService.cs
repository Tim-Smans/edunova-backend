using AutoMapper;
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
    public class TagService : ITagService
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantProvider _tenantProvider;

        public TagService(IMapper mapper, IUnitOfWork unitOfWork, ITenantProvider tenantProvider)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _tenantProvider = tenantProvider;
        }

        public async Task<ReadTagDTO> CreateTag(CreateTagDTO createTagDTO)
        {

            Tag tag = _mapper.Map<Tag>(createTagDTO);

            tag.TenantId = _tenantProvider.TenantId;
            tag.Id = Guid.NewGuid();

            await _unitOfWork.TagRepo.AddAsync(tag);
            await _unitOfWork.TagRepo.SaveAsync();

            ReadTagDTO readTagDTO = _mapper.Map<ReadTagDTO>(tag);

            return readTagDTO;
        }

        public async Task<ReadTagDTO> ReadTagById(Guid id)
        {
            Tag? tag = await _unitOfWork.TagRepo.GetByIdAsync(id);

            if (tag == null)
            {
                throw new KeyNotFoundException("Unable to find tag with that id");
            }

            ReadTagDTO tagDTO = _mapper.Map<ReadTagDTO>(tag);

            return tagDTO;
        }
    }
}
