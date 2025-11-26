using HospitalManagementSystem.Database.Data;
using HospitalManagementSystem.Models.DatabaseEntity.User;
using HospitalManagementSystem.Services.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace HospitalManagementSystem.Services.Service;

public class ServiceManager : IServiceManager
{
  private readonly HMSDbContext _db;

  public IAuthService AuthService { get; private set; }
  public ICodeGeneratorService GeneratorCodeService { get; private set; }


  public ServiceManager(HMSDbContext db, IConfiguration configuration, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
  {
    _db = db;
    var secretKey = configuration["TokenSetting:SecretKey"] ?? "";
    Console.WriteLine(secretKey);
    AuthService = new AuthService(_db, userManager, secretKey);
    GeneratorCodeService = new CodeGeneratorService(userManager);
  }


  public async Task<int> Save()
  {
    return await _db.SaveChangesAsync();
  }

}