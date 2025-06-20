using AutoMapper;
using EduNova.Core.DTO.Tenant;
using EduNova.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Infrastructure.Mapping
{
    public class MapperProfile: Profile
    {

        public MapperProfile()
        {
            CreateMap<Tenant, ReadTenantDTO>();
        }
    }
}
