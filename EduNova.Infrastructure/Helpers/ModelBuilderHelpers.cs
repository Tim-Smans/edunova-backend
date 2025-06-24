using EduNova.Infrastructure.Entities.Tenancy;
using Microsoft.EntityFrameworkCore;

namespace EduNova.Infrastructure.Helpers
{
    public static class ModelBuilderHelpers
    {

        public static void SeedTables(ModelBuilder builder)
        {

            builder.Entity<Tenant>().HasData(
                    new Tenant
                    {
                        EmailDomain = "@duckit.com",
                        Name = "DuckIT",
                        Id = SeedGuids.DuckItTenantId,
                    },
                    new Tenant
                    {
                        EmailDomain = "@example.be",
                        Name = "Example",
                        Id = SeedGuids.ExampleTenantId,
                    }
                );

            builder.Entity<HouseStyle>().HasData(
                    new HouseStyle
                    {
                        Id = SeedGuids.DuckItHouseStyleId,
                        LogoPath = "DuckIt.Path",
                        PrimaryColor = "#F1C00B",
                        TenantId = SeedGuids.DuckItTenantId,
                    },
                    new HouseStyle
                    {
                        Id = SeedGuids.ExampleHouseStyleId,
                        LogoPath = "Example.Path",
                        PrimaryColor = "#512666",
                        TenantId = SeedGuids.ExampleTenantId,
                    }
                );
        }
    }

    public static class SeedGuids
    {
        public static readonly Guid DuckItTenantId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        public static readonly Guid ExampleTenantId = Guid.Parse("22222222-2222-2222-2222-222222222222");

        public static readonly Guid DuckItHouseStyleId = Guid.Parse("aaaaaaa1-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
        public static readonly Guid ExampleHouseStyleId = Guid.Parse("bbbbbbb2-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
    }

}
