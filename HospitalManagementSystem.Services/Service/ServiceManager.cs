using HospitalManagementSystem.Database.Data;
using HospitalManagementSystem.Models.DatabaseEntity.User;
using HospitalManagementSystem.Services.IService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace HospitalManagementSystem.Services.Service;

public class ServiceManager : IServiceManager
{
  private readonly HMSDbContext _db;
  private readonly IWebHostEnvironment _env;
  private readonly IHttpContextAccessor _httpContextAccessor;

  public IAuthService AuthService { get; private set; }
  public ICodeGeneratorService GeneratorCodeService { get; private set; }
  public IFileService File { get; private set; }


  public ServiceManager(HMSDbContext db, IConfiguration configuration, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
  {
    _db = db;
    _env = env;
    _httpContextAccessor = httpContextAccessor;
    var secretKey = configuration["TokenSetting:SecretKey"] ?? "";
    Console.WriteLine(secretKey);
    AuthService = new AuthService(_db, userManager, secretKey);
    GeneratorCodeService = new CodeGeneratorService(userManager);
    File = new FileService(_env, _httpContextAccessor);
  }


  public async Task<int> Save()
  {
    return await _db.SaveChangesAsync();
  }

}