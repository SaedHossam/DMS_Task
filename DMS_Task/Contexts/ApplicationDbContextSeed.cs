using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DAL.Models;
using DMS_Task.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DMS_Task.Contexts
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedEssentialsAsync(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IHost host)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Administrator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Employee.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Company.ToString()));

            //Seed Default User
            var adminUser = new ApplicationUser
            {
                UserName = Authorization.AdminUserName,
                Email = Authorization.AdminEmail,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                FirstName = "Admin",
                LastName = "Admin"
            };

            if (userManager.Users.All(u => u.Id != adminUser.Id))
            {
                await userManager.CreateAsync(adminUser, Authorization.AdminPassword);
                await userManager.AddToRoleAsync(adminUser, Authorization.AdminRole.ToString());
            }

            var companyUser = new ApplicationUser
            {
                UserName = Authorization.CompanyUserName,
                Email = Authorization.CompanyEmail,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                FirstName = "Company",
                LastName = "Company"
            };

            if (userManager.Users.All(u => u.Id != companyUser.Id))
            {
                await userManager.CreateAsync(companyUser, Authorization.CompanyPassword);
                await userManager.AddToRoleAsync(companyUser, Authorization.CompanyRole.ToString());
            }

            var employeeUser = new ApplicationUser
            {
                UserName = Authorization.EmployeeUserName,
                Email = Authorization.EmployeeEmail,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                FirstName = "Employee",
                LastName = "Employee",
            };

            if (userManager.Users.All(u => u.Id != employeeUser.Id))
            {
                await userManager.CreateAsync(employeeUser, Authorization.EmployeePassword);
                await userManager.AddToRoleAsync(employeeUser, Authorization.EmployeeRole.ToString());
            }

            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<ApplicationDbContext>();

            //if (!context.Skills.Any())
            //{
            //    var skills = new List<Skill>()
            //    {
            //        new Skill() {Name = "html"},
            //        new Skill() {Name = "css"},
            //        new Skill() {Name = "JavaScript"},
            //        new Skill() {Name = "C#"},
            //        new Skill() {Name = "bootstrap"},

            //    };

            //    await context.Skills.AddRangeAsync(skills);
            //    await context.SaveChangesAsync();
            //}

            
        }
    }
}
