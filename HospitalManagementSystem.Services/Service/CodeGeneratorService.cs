using HospitalManagementSystem.Database.Data;
using HospitalManagementSystem.Models.DatabaseEntity.User;
using HospitalManagementSystem.Services.IService;
using HospitalManagementSystem.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Services.Service;

public class CodeGeneratorService : ICodeGeneratorService
{
    private readonly UserManager<ApplicationUser>  _userManager;
    private readonly HMSDbContext _db;
    public CodeGeneratorService(UserManager<ApplicationUser> userManager, HMSDbContext db)
    {
        _userManager = userManager;
        _db = db;
    }
    public async Task<string> GenerateCodeAsync(string role)
    {
        var prefix = RoleCodeGenerator.GetRolePrefix(role);
        
        //count existing code for role
        var user = await _userManager.GetUsersInRoleAsync(role.ToString());
        var count = user.Count;
        var number = (count).ToString("D2");
        return $"{prefix}{number}";
        
    }
    
    public async Task<string> GdCodeAsync(string name)
    {
        const string prefix = "Dept";
        
        //count existing code for dept name
        var dept = await _db.Departments.ToListAsync();
        var count = dept.Count;
        var number = (count).ToString("D2");
        return $"{prefix}{number}";
        
    }
    public async Task<string> GpCodeAsync()
    {
        const string prefix = "Pat";
        
        //count existing code for dept name
        var data = await _db.Patients.ToListAsync();
        var count = data.Count;
        var number = (count).ToString("D2");
        return $"{prefix}{number}";
        
    }
}