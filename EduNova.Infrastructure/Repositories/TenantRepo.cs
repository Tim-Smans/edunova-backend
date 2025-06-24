using EduNova.Infrastructure.Entities.Tenancy;
using EduNova.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Infrastructure.Repositories
{
    public class TenantRepo : GenericRepo<Tenant>, ITenantRepo
    {
        public TenantRepo(NovaDBContext context) : base(context) { }

        public async Task<Tenant> GetByNameAsync(string name)
        {
            return await _context
                .Tenants
                .Where(x => x.Name.ToLower() == name.ToLower())
                .FirstOrDefaultAsync();
                
        }
    }
}
