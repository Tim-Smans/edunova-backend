using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Infrastructure.Entities
{
    public class CustomUser : IdentityUser
    {

        public Guid? TenantId { get; set; }
        public Tenant Tenant { get; set; }
    }
}
