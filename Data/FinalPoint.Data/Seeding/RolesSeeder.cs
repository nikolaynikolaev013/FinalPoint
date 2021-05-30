namespace FinalPoint.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using FinalPoint.Common;
    using FinalPoint.Data.Models;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    internal class RolesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            await SeedRoleAsync(roleManager, GlobalConstants.AdministratorRoleName);
            await SeedRoleAsync(roleManager, GlobalConstants.OfficeAdminRoleName);
            await SeedRoleAsync(roleManager, GlobalConstants.SortingCenterAdminRoleName);
            await SeedRoleAsync(roleManager, GlobalConstants.OwnerRoleName);
            await SeedRoleAsync(roleManager, GlobalConstants.OfficeOwnerRoleName);
            await SeedRoleAsync(roleManager, GlobalConstants.SortingCenterManagerRoleName);
            await SeedRoleAsync(roleManager, GlobalConstants.OfficeManagerRoleName);
            await SeedRoleAsync(roleManager, GlobalConstants.DriverRoleName);

        }

        private static async Task SeedRoleAsync(RoleManager<ApplicationRole> roleManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                var result = await roleManager.CreateAsync(new ApplicationRole(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
