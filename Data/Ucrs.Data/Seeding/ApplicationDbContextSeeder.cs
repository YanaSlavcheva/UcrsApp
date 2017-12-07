namespace Ucrs.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Ucrs.Common;
    using Ucrs.Data.Models;

    public static class ApplicationDbContextSeeder
    {
        public static async Task Seed(
            ApplicationDbContext dbContext,
            IServiceProvider serviceProvider)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            await Seed(dbContext, roleManager, userManager);
        }

        public static async Task Seed(
            ApplicationDbContext dbContext,
            RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (roleManager == null)
            {
                throw new ArgumentNullException(nameof(roleManager));
            }

            await SeedRoles(roleManager);
            await SeedUsers(userManager);
        }

        private static async Task SeedRoles(RoleManager<ApplicationRole> roleManager)
        {
            await SeedRole(GlobalConstants.AdministratorRoleName, roleManager);
        }

        private static async Task SeedRole(string roleName, RoleManager<ApplicationRole> roleManager)
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

        private static async Task SeedUsers(UserManager<ApplicationUser> userManager)
        {
            var adminUser = new ApplicationUser()
            {
                UserName = GlobalConstants.AdministratorUserName,
                Email = GlobalConstants.AdministratorUserName
            };

            await userManager.CreateAsync(adminUser, GlobalConstants.AdministratorPassword);
            await userManager.AddToRoleAsync(adminUser, GlobalConstants.AdministratorRoleName);
        }
    }
}
