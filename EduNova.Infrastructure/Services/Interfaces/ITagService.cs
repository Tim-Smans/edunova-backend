using EduNova.Core.DTO.Tag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Infrastructure.Services.Interfaces
{
    public interface ITagService
    {

        Task<ReadTagDTO> ReadTagById(Guid id);
        Task<ReadTagDTO> CreateTag(CreateTagDTO createTagDTO);
    }
}
