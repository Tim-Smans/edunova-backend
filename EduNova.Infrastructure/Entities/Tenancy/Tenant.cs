using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Infrastructure.Entities.Tenancy
{
    public class Tenant
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string EmailDomain { get; set; }

        public ICollection<CustomUser>? Users {  get; set; }
        public HouseStyle? HouseStyle { get; set; }
    }
}
