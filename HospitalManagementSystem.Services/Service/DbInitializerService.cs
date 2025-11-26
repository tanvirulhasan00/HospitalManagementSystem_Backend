using HospitalManagementSystem.Database.Data;
using HospitalManagementSystem.Models.DatabaseEntity.User;
using HospitalManagementSystem.Services.IService;
using HospitalManagementSystem.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Services.Service;

public class DbInitializerService : IDbInitializerService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly HMSDbContext _dbContext;
    private readonly IServiceManager _serviceManager;

    public DbInitializerService(UserManager<ApplicationUser> userManager, 
        RoleManager<IdentityRole> roleManager, 
        HMSDbContext dbContext
        , IServiceManager serviceManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _dbContext = dbContext;
        _serviceManager = serviceManager;
    }

    public async Task InitializeAsync()
    {
        await ApplyMigrationsAsync();
        await SeedRolesAsync();
        await SeedAdminUserAsync();
    }

    private async Task ApplyMigrationsAsync()
    {
        if ((await _dbContext.Database.GetPendingMigrationsAsync()).Any())
        {
            await _dbContext.Database.MigrateAsync();
        }
        else
        {
            Console.WriteLine("ℹ️ No pending migrations.");
        }
    }
    private async Task SeedRolesAsync()
    {
        string[] roles = { 
            RoleVariable.Admin,
            RoleVariable.Receptionist,
            RoleVariable.Doctor, 
            RoleVariable.Nurse, 
            RoleVariable.Accountant,
            RoleVariable.Laboratorist,
            RoleVariable.Pharmacist
            
        };

        foreach (var role in roles)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    private async Task SeedAdminUserAsync()
    {
        var stuffCodeG = await _serviceManager.GeneratorCodeService.GenerateCodeAsync(Role.Admin);

        const string adminEmail = "admin@gmail.com";
        const string adminPassword = "aDmin@00#";
        const string adminUsername = "admin";
        const string phoneNumber = "01970806028";
        var stuffCode = stuffCodeG;

        var existingAdmin = await _userManager.FindByNameAsync(adminUsername);

        if (existingAdmin == null)
        {
            var adminUser = new ApplicationUser
            {
                UserName = adminUsername,
                Email = adminEmail,
                PhoneNumber = phoneNumber,
                Password = adminPassword,
                StuffCode =  stuffCode
            };

            var result = await _userManager.CreateAsync(adminUser, adminPassword);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(adminUser, RoleVariable.Admin);
            }
            else
            {
                throw new InvalidOperationException(
                    $"Failed to create admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }
    }
}