using EduNova.Infrastructure.Entities;
using EduNova.Infrastructure.Entities.Courses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Infrastructure.Helpers
{
    public class IdentitySeeding
    {
        public async Task IdentitySeedingAsync(
            UserManager<CustomUser> userManager,
            RoleManager<IdentityRole> roleManager,
            NovaDBContext db)
        {
            const string DefaultPassword = "Password123!";

            await CreateRoles(roleManager);

            // --- USERS ---
            // DuckIT
            var duckitAdmin = await EnsureUser(userManager, "DuckitAdmin", "admin@duckit.com", SeedGuids.DuckItTenantId, DefaultPassword);
            await EnsureInRole(userManager, duckitAdmin, "admin");

            var duckitManager = await EnsureUser(userManager, "DuckitManager", "manager@duckit.com", SeedGuids.DuckItTenantId, DefaultPassword);
            await EnsureInRole(userManager, duckitManager, "manager");

            var duckitAuthor = await EnsureUser(userManager, "DuckitAuthor", "author@duckit.com", SeedGuids.DuckItTenantId, DefaultPassword);
            await EnsureInRole(userManager, duckitAuthor, "author");

            var duckitUser = await EnsureUser(userManager, "DuckitUser", "user@duckit.com", SeedGuids.DuckItTenantId, DefaultPassword);

            // Example
            var exampleUser = await EnsureUser(userManager, "ExampleUser", "user@example.be", SeedGuids.ExampleTenantId, DefaultPassword);

            // SysAdmin (tenant = ExampleTenantId in jouw code)
            var sysAdmin = await EnsureUser(userManager, "SysAdmin", "admin@edunova.com", SeedGuids.ExampleTenantId, DefaultPassword);
            await EnsureInRole(userManager, sysAdmin, "sysAdmin");

            // --- COURSE ASSIGNMENTS ---
            // Zorg dat Tenants/Courses via migratie al bestaan; nu kunnen we koppelen op basis van echte user.Id
            await EnsureCourseAssigned(db,
                id: SeedGuids.DuckItAssigned1Id,
                tenantId: SeedGuids.DuckItTenantId,
                userId: duckitAdmin.Id,
                courseId: SeedGuids.DuckItCourseJs101Id);

            await EnsureCourseAssigned(db,
                id: SeedGuids.ExampleAssigned1Id,
                tenantId: SeedGuids.ExampleTenantId,
                userId: exampleUser.Id,
                courseId: SeedGuids.ExampleCourseSqlId);
        }
        private async Task<CustomUser> EnsureUser(
            UserManager<CustomUser> userManager,
            string userName,
            string email,
            Guid tenantId,
            string password)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user != null) return user;

            user = new CustomUser
            {
                UserName = userName,
                Email = email,
                EmailConfirmed = true,
                TenantId = tenantId
            };

            var result = await userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                throw new Exception($"Failed creating user {userName}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            return user;
        }

        private async Task EnsureInRole(UserManager<CustomUser> userManager, CustomUser user, string role)
        {
            if (!await userManager.IsInRoleAsync(user, role))
                await userManager.AddToRoleAsync(user, role);
        }

        private async Task EnsureCourseAssigned(NovaDBContext db, Guid id, Guid tenantId, string userId, Guid courseId)
        {
            // Idempotent: als hij er al is (op Id of combi), niks doen
            var exists = await db.CoursesAssigned
                .AnyAsync(x => x.Id == id || (x.TenantId == tenantId && x.UserId == userId && x.CourseId == courseId));
            if (exists) return;

            db.CoursesAssigned.Add(new CourseAssigned
            {
                Id = id,
                TenantId = tenantId,
                UserId = userId,
                CourseId = courseId
            });
            await db.SaveChangesAsync();
        }

        private async Task CreateRoles(RoleManager<IdentityRole> roleManager)
        {
            async Task TryCreateRole(string roleName)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    var result = await roleManager.CreateAsync(new IdentityRole(roleName));
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine($"[Role Error] {roleName}: {error.Description}");
                        }
                    }
                }
            }

            await TryCreateRole("author");
            await TryCreateRole("manager");
            await TryCreateRole("admin");
            await TryCreateRole("sysAdmin");
        }
    }
}
