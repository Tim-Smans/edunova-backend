using EduNova.Infrastructure.MultiTenancy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Infrastructure
{
    public class NovaDBContextFactory : IDesignTimeDbContextFactory<NovaDBContext>
    {
        public NovaDBContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json", optional: false)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<NovaDBContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("LocalDBConnection"));

            return new NovaDBContext(optionsBuilder.Options, new DesignTimeTenantProvider());
        }
    }

    public class DesignTimeTenantProvider : ITenantProvider
    {
        public Guid TenantId => Guid.Parse("11111111-1111-1111-1111-111111111111");
    }
}
