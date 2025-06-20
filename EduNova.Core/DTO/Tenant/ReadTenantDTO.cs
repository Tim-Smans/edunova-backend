using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Core.DTO.Tenant
{
    public class ReadTenantDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string EmailDomain { get; set; }
    }
}
