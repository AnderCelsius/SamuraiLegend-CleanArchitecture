using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using SamuraiLegend.Domain.Entities;
using SamuraiLegend.Infrastructure.Persistence.Contexts;
using SamuraiLegend.Infrastructure.Persistence.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SamuraiLegend.Infrastructure.Persistence.Seeds
{
    public class Seeder
    {
        public static async Task SeedData(ApplicationDbContext dbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var baseDir = Directory.GetCurrentDirectory();

            await dbContext.Database.EnsureCreatedAsync();


            // Samurais and Quotes
            if (!dbContext.Samurais.Any())
            {
                var path = File.ReadAllText(FilePath(baseDir, "Json/Samurais.json"));

                var samurais = JsonConvert.DeserializeObject<List<Samurai>>(path);
                await dbContext.Samurais.AddRangeAsync(samurais);
            }

            if (!dbContext.Users.Any())
            {
                List<string> roles = new() { "Admin", "Regular" };

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = role });
                }

                var user = new AppUser
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = "Obinna",
                    LastName = "Asiegbulam",
                    Email = "oasiegbulam@gmail.com",
                    PhoneNumber = "08036021425",
                    PublicId = null,
                    Avatar = "https://www.pngfind.com/pngs/m/676-6764065_default-profile-picture-transparent-hd-png-download.png",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
            };
                user.UserName = user.Email;
                
                await userManager.CreateAsync(user, "Password@123");
                await userManager.AddToRoleAsync(user, "Admin");
            }


            await dbContext.SaveChangesAsync();
        }

        static string FilePath(string folderName, string fileName)
        {
            return Path.Combine(folderName, fileName);
        }
    }
}
