using api.Models;
using Microsoft.AspNetCore.Identity;

namespace api.Services
{
    public static class SeedingService
    {
        public static async Task SeedUser(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string userEmail = "user@example.com";
            string userPassword = "string1";

            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            var user = await userManager.FindByEmailAsync(userEmail);

            if (user == null)
            {
                user = new User
                {
                    UserName = userEmail,
                    Email = userEmail,
                    Name = "User",
                    Theme = Theme.Light,
                };

                var result = await userManager.CreateAsync(user, userPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "User");
                }
            }
        }
        public static async Task SeedEmployee(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string employeeEmail = "manager@example.com";
            string employeePassword = "string1";

            if (!await roleManager.RoleExistsAsync("Employee"))
            {
                await roleManager.CreateAsync(new IdentityRole("Employee"));
            }

            var user = await userManager.FindByEmailAsync(employeeEmail);

            if (user == null)
            {
                user = new User
                {
                    UserName = employeeEmail,
                    Email = employeeEmail,
                    Name = "Employee",
                    Theme = Theme.Light,
                };

                var result = await userManager.CreateAsync(user, employeePassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Employee");
                }
            }
        }
    }
}