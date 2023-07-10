using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace SkillProfiApi.Models
{
    public class SeedData
    {
        private const string User = "Admin";
        private const string Pass = "Admin";
        public static async void CreateMigrationBase(IApplicationBuilder app)
        {
             var identityCon = app.ApplicationServices.CreateScope().ServiceProvider
                                                      .GetRequiredService<IdentityContext>();
            if(identityCon.Database.GetPendingMigrations().Any())
            {
                identityCon.Database.Migrate();
            }
            UserManager<IdentityUser> manager = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<UserManager<IdentityUser>>();
            RoleManager<IdentityRole> _roleManager = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();
            IdentityUser user = await manager.FindByNameAsync(User);
            await _roleManager.CreateAsync(new IdentityRole("Admin"));
            await _roleManager.CreateAsync(new IdentityRole("Admin"));
            if (user == null)
            {
                user = new IdentityUser(User);
                await manager.CreateAsync(user, Pass);
                await manager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
