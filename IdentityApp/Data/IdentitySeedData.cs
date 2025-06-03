using IdentityApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityApp.Data;

public static class IdentitySeedData
{
    private const string adminUser = "Admin";
    private const string adminPassword = "Admin_123";

    public static async void IdentityTestUser(IApplicationBuilder app)
    {
        var context = app.ApplicationServices.CreateAsyncScope().ServiceProvider.GetRequiredService<IdentityContext>();

        if (context.Database.GetAppliedMigrations().Any())
        {
            context.Database.Migrate();
        }

        var userManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<UserManager<AppUser>>();


        var user = await userManager.FindByNameAsync(adminUser);

        if (user == null)
        {
            user = new AppUser
            {
                FullName = "Kerem Can Kırpar",
                UserName = adminUser,
                Email = "admin@keremcan.com",
                PhoneNumber = "444444444"
            };

            await userManager.CreateAsync(user, adminPassword);
        }
    }
}