using EduNova.Infrastructure.Entities;
using EduNova.Infrastructure.Entities.CourseModules;
using EduNova.Infrastructure.Entities.CourseModules.Quiz;
using EduNova.Infrastructure.Entities.Courses;
using EduNova.Infrastructure.Entities.Modules;
using EduNova.Infrastructure.Entities.Tenancy;
using EduNova.Infrastructure.Helpers;
using EduNova.Infrastructure.MultiTenancy;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseAssigned> CoursesAssigned { get; set; }
        public DbSet<CourseTag> CourseTags { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<ModuleText> ModuleTexts { get; set; }
        public DbSet<ModuleAssignment> ModuleAssignments { get; set; }
        public DbSet<ModulePowerpoint> ModulePowerpoints { get; set; }
        public DbSet<ModuleVideo> ModuleVideos { get; set; }
        public DbSet<ModuleQuiz> ModuleQuizzes { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }





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

            builder.Entity<CourseTag>()
                .HasOne(x => x.Course)
                .WithMany( x => x.CourseTags)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CourseTag>()
                .HasOne(x => x.Tag)
                .WithMany(x => x.CourseTags)
                .HasForeignKey(x => x.TagId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CourseAssigned>()
                .HasOne<Course>()
                .WithMany()
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<CourseAssigned>()
                .HasOne<CustomUser>()
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Module>()
                .HasOne<Course>()
                .WithMany()
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<QuizQuestion>()
                .HasOne<ModuleQuiz>()
                .WithMany(x => x.Questions)
                .HasForeignKey(x => x.QuizId)
                .OnDelete(DeleteBehavior.Cascade);
        }
        

        private void InitTables(ModelBuilder builder)
        {
            builder.Entity<CustomUser>().ToTable("CustomUsers");
            builder.Entity<HouseStyle>().ToTable("HouseStyles");
            builder.Entity<Tenant>().ToTable("Tenants");
            builder.Entity<Course>().ToTable("Courses");
            builder.Entity<CourseAssigned>().ToTable("CoursesAssigned");
            builder.Entity<CourseTag>().ToTable("CourseTags");
            builder.Entity<Tag>().ToTable("Tags");
            builder.Entity<Module>().ToTable("Modules");
            builder.Entity<ModuleVideo>().ToTable("ModuleVideos");
            builder.Entity<ModuleText>().ToTable("ModuleTexts");
            builder.Entity<ModulePowerpoint>().ToTable("ModulePowerpoints");
            builder.Entity<ModuleAssignment>().ToTable("ModuleAssignments");
            builder.Entity<ModuleQuiz>().ToTable("ModuleQuizzes");
            builder.Entity<QuizQuestion>().ToTable("QuizQuestions");

            builder.Entity<CustomUser>().HasQueryFilter(c => c.TenantId == _tenantProvider.TenantId);
            builder.Entity<HouseStyle>().HasQueryFilter(h => h.TenantId == _tenantProvider.TenantId);
            builder.Entity<Course>().HasQueryFilter(x => x.TenantId == _tenantProvider.TenantId);
            builder.Entity<CourseAssigned>().HasQueryFilter(x => x.TenantId == _tenantProvider.TenantId);
            builder.Entity<CourseTag>().HasQueryFilter(x => x.TenantId == _tenantProvider.TenantId);
            builder.Entity<Tag>().HasQueryFilter(x => x.TenantId == _tenantProvider.TenantId);
            builder.Entity<Module>().HasQueryFilter(x => x.TenantId == _tenantProvider.TenantId);
            builder.Entity<QuizQuestion>().HasQueryFilter(x => x.TenantId == _tenantProvider.TenantId);

        }
    }
}
 