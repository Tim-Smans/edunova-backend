using EduNova.Infrastructure.Entities;
using EduNova.Infrastructure.Helpers;
using EduNova.Infrastructure.MultiTenancy;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Infrastructure
{
    public class NovaDBContext : IdentityDbContext<CustomUser>
    {
        private readonly ITenantProvider _tenantProvider;

        public NovaDBContext(DbContextOptions<NovaDBContext> options, ITenantProvider tenantProvider) : base(options)
        {
            _tenantProvider = tenantProvider;
        }

        public DbSet<CustomUser> Users {  get; set; }
        public DbSet<HouseStyle> HouseStyles { get; set; }
        public DbSet<Tenant> Tenants { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            // Initialise relationships
            InitRelationships(builder);

            // Initialise tables
            InitTables(builder);

            // Seed dummy data
            ModelBuilderHelpers.SeedTables(builder);
        }

        private void InitRelationships(ModelBuilder builder)
        {
            builder.Entity<CustomUser>()
                .HasOne(u => u.Tenant)
                .WithMany(t => t.Users)
                .HasForeignKey(u => u.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<HouseStyle>()
                .HasOne(hs => hs.Tenant)
                .WithOne(t => t.HouseStyle)
                .HasForeignKey<HouseStyle>(x => x.TenantId)
                .OnDelete(DeleteBehavior.SetNull);
        }
        

        private void InitTables(ModelBuilder builder)
        {
            builder.Entity<CustomUser>().ToTable("CustomUsers");
            builder.Entity<HouseStyle>().ToTable("HouseStyles");
            builder.Entity<Tenant>().ToTable("Tenants");

            builder.Entity<CustomUser>().HasQueryFilter(c => c.TenantId == _tenantProvider.TenantId);
            builder.Entity<HouseStyle>().HasQueryFilter(h => h.TenantId == _tenantProvider.TenantId);

        }
    }
}
 