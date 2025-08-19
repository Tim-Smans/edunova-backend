using EduNova.Infrastructure.Entities.Tenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Infrastructure.Repositories.Interfaces
{
    public interface ITenantRepo : IGenericRepo<Tenant>
    {
        Task<Tenant> GetByNameAsync(string name);
    }
}
