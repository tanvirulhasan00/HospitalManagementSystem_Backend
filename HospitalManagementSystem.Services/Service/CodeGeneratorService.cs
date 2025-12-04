using HospitalManagementSystem.Database.Data;
using HospitalManagementSystem.Models.DatabaseEntity.User;
using HospitalManagementSystem.Services.IService;
using HospitalManagementSystem.Utilities;
using Microsoft.AspNetCore.Identity;

namespace HospitalManagementSystem.Services.Service;

public class CodeGeneratorService : ICodeGeneratorService
{
    private readonly UserManager<ApplicationUser>  _userManager;
    public CodeGeneratorService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
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
}