using EduNova.Infrastructure.Entities.Courses;
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

            // ---------- TAGS ----------
            builder.Entity<Tag>().HasData(
                // DuckIT
                new Tag { Id = SeedGuids.DuckItTagProgId, Title = "Programming", TenantId = SeedGuids.DuckItTenantId },
                new Tag { Id = SeedGuids.DuckItTagFrontendId, Title = "Frontend", TenantId = SeedGuids.DuckItTenantId },

                // Example
                new Tag { Id = SeedGuids.ExampleTagDataId, Title = "Data", TenantId = SeedGuids.ExampleTenantId },
                new Tag { Id = SeedGuids.ExampleTagBackendId, Title = "Backend", TenantId = SeedGuids.ExampleTenantId }
            );

            // ---------- COURSES ----------
            builder.Entity<Course>().HasData(
                // DuckIT
                new Course
                {
                    Id = SeedGuids.DuckItCourseJs101Id,
                    TenantId = SeedGuids.DuckItTenantId,
                    Title = "JavaScript 101",
                    Description = "Introductie tot JS: syntax, types, DOM, fetch API.",
                    Category = "Programming",
                    TargetAudience = "Students/Interns",
                    Published = true,
                    Public = true,
                    IsSequential = true,
                },
                new Course
                {
                    Id = SeedGuids.DuckItCourseCsharpId,
                    TenantId = SeedGuids.DuckItTenantId,
                    Title = "C# Fundamentals",
                    Description = "Basis C#: OOP, LINQ, async/await, best practices.",
                    Category = "Programming",
                    TargetAudience = "Jr Developers",
                    Published = false,
                    Public = false,
                    IsSequential = false,
                },

                // Example
                new Course
                {
                    Id = SeedGuids.ExampleCourseSqlId,
                    TenantId = SeedGuids.ExampleTenantId,
                    Title = "SQL Basics",
                    Description = "Selects, joins, aggregaties en indexing-intro.",
                    Category = "Data",
                    TargetAudience = "Analysts",
                    Published = true,
                    Public = true,
                    IsSequential = true,
                },
                new Course
                {
                    Id = SeedGuids.ExampleCoursePythonId,
                    TenantId = SeedGuids.ExampleTenantId,
                    Title = "Python for Backends",
                    Description = "Python essentials, API’s met FastAPI, testing.",
                    Category = "Backend",
                    TargetAudience = "Developers",
                    Published = true,
                    Public = false,
                    IsSequential = false,
                }
            );

            // ---------- COURSE TAGS (koppel-tabellen) ----------
            builder.Entity<CourseTag>().HasData(
                // DuckIT
                new CourseTag
                {
                    Id = SeedGuids.DuckItCt1Id,
                    TenantId = SeedGuids.DuckItTenantId,
                    CourseId = SeedGuids.DuckItCourseJs101Id,
                    TagId = SeedGuids.DuckItTagProgId
                },
                new CourseTag
                {
                    Id = SeedGuids.DuckItCt2Id,
                    TenantId = SeedGuids.DuckItTenantId,
                    CourseId = SeedGuids.DuckItCourseJs101Id,
                    TagId = SeedGuids.DuckItTagFrontendId
                },
                new CourseTag
                {
                    Id = SeedGuids.DuckItCt3Id,
                    TenantId = SeedGuids.DuckItTenantId,
                    CourseId = SeedGuids.DuckItCourseCsharpId,
                    TagId = SeedGuids.DuckItTagProgId
                },

                // Example
                new CourseTag
                {
                    Id = SeedGuids.ExampleCt1Id,
                    TenantId = SeedGuids.ExampleTenantId,
                    CourseId = SeedGuids.ExampleCourseSqlId,
                    TagId = SeedGuids.ExampleTagDataId
                },
                new CourseTag
                {
                    Id = SeedGuids.ExampleCt2Id,
                    TenantId = SeedGuids.ExampleTenantId,
                    CourseId = SeedGuids.ExampleCoursePythonId,
                    TagId = SeedGuids.ExampleTagBackendId
                },
                new CourseTag
                {
                    Id = SeedGuids.ExampleCt3Id,
                    TenantId = SeedGuids.ExampleTenantId,
                    CourseId = SeedGuids.ExampleCoursePythonId,
                    TagId = SeedGuids.ExampleTagDataId
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

        // Admin users (PAS AAN naar je echte Identity User IDs)
        public const string AdminDuckItUserId = "67adad1a-955f-4cdc-a430-4912be46c214";
        public const string ExampleUserId = "66c965d2-e658-4261-8111-b04197cbba06";

        // ---------- DuckIT ----------
        // Courses
        public static readonly Guid DuckItCourseJs101Id = Guid.Parse("33333333-3333-3333-3333-333333333331");
        public static readonly Guid DuckItCourseCsharpId = Guid.Parse("33333333-3333-3333-3333-333333333332");

        // Tags
        public static readonly Guid DuckItTagProgId = Guid.Parse("44444444-4444-4444-4444-444444444441");
        public static readonly Guid DuckItTagFrontendId = Guid.Parse("44444444-4444-4444-4444-444444444442");

        // CourseTags
        public static readonly Guid DuckItCt1Id = Guid.Parse("55555555-5555-5555-5555-555555555551");
        public static readonly Guid DuckItCt2Id = Guid.Parse("55555555-5555-5555-5555-555555555552");
        public static readonly Guid DuckItCt3Id = Guid.Parse("55555555-5555-5555-5555-555555555553");

        // CourseAssigned
        public static readonly Guid DuckItAssigned1Id = Guid.Parse("66666666-6666-6666-6666-666666666661");

        // ---------- Example ----------
        // Courses
        public static readonly Guid ExampleCourseSqlId = Guid.Parse("33333333-3333-3333-3333-333333333341");
        public static readonly Guid ExampleCoursePythonId = Guid.Parse("33333333-3333-3333-3333-333333333342");

        // Tags
        public static readonly Guid ExampleTagDataId = Guid.Parse("44444444-4444-4444-4444-444444444451");
        public static readonly Guid ExampleTagBackendId = Guid.Parse("44444444-4444-4444-4444-444444444452");

        // CourseTags
        public static readonly Guid ExampleCt1Id = Guid.Parse("55555555-5555-5555-5555-555555555561");
        public static readonly Guid ExampleCt2Id = Guid.Parse("55555555-5555-5555-5555-555555555562");
        public static readonly Guid ExampleCt3Id = Guid.Parse("55555555-5555-5555-5555-555555555563");

        // CourseAssigned
        public static readonly Guid ExampleAssigned1Id = Guid.Parse("66666666-6666-6666-6666-666666666671");
    }

}
