using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Infrastructure.Entities
{
    public class Tenant
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string EmailDomain { get; set; }

        public ICollection<CustomUser> Users {  get; set; }
        public HouseStyle HouseStyle { get; set; }
    }
}
