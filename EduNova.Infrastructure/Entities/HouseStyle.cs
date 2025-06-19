using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Infrastructure.Entities
{
    public class HouseStyle
    {

        public Guid Id {  get; set; }
        public string LogoPath { get; set; }
        public string PrimaryColor { get; set; }

        public Guid? TenantId { get; set; }
        public Tenant Tenant { get; set; }
    }
}
