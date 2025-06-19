using EduNova.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
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
        public async Task IdentitySeedingAsync(UserManager<CustomUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            const string DefaultPassword = "Password123!";

            try
            {
                await CreateRoles(roleManager);

                // Create users

                //Duckit
                if (await userManager.FindByNameAsync("DuckitAdmin") == null)
                {
                    var duckitAdmin = new CustomUser
                    {
                        UserName = "DuckitAdmin",
                        Email = "admin@duckit.com",
                        EmailConfirmed = true,
                        TenantId = SeedGuids.DuckItTenantId,
                    };

                    await userManager.CreateAsync(duckitAdmin, DefaultPassword);
                    await userManager.AddToRoleAsync(duckitAdmin, "admin");

                }

                if (await userManager.FindByNameAsync("DuckitManager") == null)
                {
                    var duckitManager = new CustomUser
                    {
                        UserName = "DuckitManager",
                        Email = "manager@duckit.com",
                        EmailConfirmed = true,
                        TenantId = SeedGuids.DuckItTenantId,
                    };

                    await userManager.CreateAsync(duckitManager, DefaultPassword);
                    await userManager.AddToRoleAsync(duckitManager, "manager");

                }

                if (await userManager.FindByNameAsync("DuckitAuthor") == null)
                {
                    var duckitAuthor = new CustomUser
                    {
                        UserName = "DuckitAuthor",
                        Email = "author@duckit.com",
                        EmailConfirmed = true,
                        TenantId = SeedGuids.DuckItTenantId,
                    };

                    await userManager.CreateAsync(duckitAuthor, DefaultPassword);
                    await userManager.AddToRoleAsync(duckitAuthor, "author");

                }

                if (await userManager.FindByNameAsync("DuckitUser") == null)
                {
                    var duckitUser = new CustomUser
                    {
                        UserName = "DuckitUser",
                        Email = "user@duckit.com",
                        EmailConfirmed = true,
                        TenantId = SeedGuids.DuckItTenantId,
                    };

                    await userManager.CreateAsync(duckitUser, DefaultPassword);
                }

                //Example
                if (await userManager.FindByNameAsync("ExampleUser") == null)
                {
                    var exampleUser = new CustomUser
                    {
                        UserName = "ExampleUser",
                        Email = "user@example.be",
                        EmailConfirmed = true,
                        TenantId = SeedGuids.ExampleTenantId,
                    };

                    await userManager.CreateAsync(exampleUser, DefaultPassword);
                }

                //System admin
                if (await userManager.FindByNameAsync("SysAdmin") == null)
                { 
                    var sysAdmin = new CustomUser
                    {
                        UserName = "SysAdmin",
                        Email = "admin@edunova.com",
                        EmailConfirmed = true,
                        TenantId = SeedGuids.ExampleTenantId,
                    };

                    await userManager.CreateAsync(sysAdmin, DefaultPassword);
                    await userManager.AddToRoleAsync(sysAdmin, "sysAdmin");

                }
            }
            catch (DbException ex)
            {
                throw new Exception(ex.Message.ToString());
            }
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
