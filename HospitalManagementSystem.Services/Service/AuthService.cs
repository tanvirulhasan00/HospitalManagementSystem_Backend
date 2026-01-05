using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using HospitalManagementSystem.Database.Data;
using HospitalManagementSystem.Models.DatabaseEntity.User;
using HospitalManagementSystem.Models.DatabaseEntity.User.Dto;
using HospitalManagementSystem.Models.GenericModels;
using HospitalManagementSystem.Services.IService;
using HospitalManagementSystem.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace HospitalManagementSystem.Services.Service;

public class AuthService : Service<ApplicationUser>, IAuthService
{
    private readonly HMSDbContext _db;
    private readonly ApiResponse _response;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IWebHostEnvironment _env;
    private readonly IHttpContextAccessor _httpContextAccessor;
    //private readonly RoleManager<IdentityRole> _roleManager;
    private readonly string _secretKey;

    public AuthService(
        HMSDbContext db,
        UserManager<ApplicationUser> userManager,
        string secretKey,
        IWebHostEnvironment env,
        IHttpContextAccessor httpContextAccessor
    ) : base(db)
    {
        _db = db;
        _response = new ApiResponse();
        _userManager = userManager;
        _secretKey = secretKey;
        _env = env;
        _httpContextAccessor = httpContextAccessor;
        
    }
    public async Task<bool> IsPhoneNumberUnique(string phoneNumber)
    {
        return !await _db.ApplicationUsers.AnyAsync(u => u.PhoneNumber == phoneNumber);
    }
    public async Task<bool> IsEmailUnique(string email)
    {
        return !await _db.ApplicationUsers.AnyAsync(u => u.Email == email);
    }
    public async Task<bool> IsUserUnique(string userName)
    {
        return !await _db.ApplicationUsers.AnyAsync(u => u.UserName == userName);
    }

    public async Task<ApiResponse> Login(LoginRequest request)
    {
        var loginResponse = new LoginResponse();
        try
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                _response.Success = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Message = "Username or password is incorrect.";
                return _response;
            }
            var user = await _db.ApplicationUsers.FirstOrDefaultAsync(u => u.UserName == request.Username);
            if (user == null)
            {
                _response.Success = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Message = "Username or password is incorrect.";
                return _response;
            }
            var isValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isValid)
            {
                _response.Success = false;
                _response.StatusCode = HttpStatusCode.Unauthorized;
                _response.Message = "Username or password is incorrect.";
                return _response;
            }

            //roles 
            var roles = await _userManager.GetRolesAsync(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var tokenExpire = request.RememberMe ? DateTime.UtcNow.AddDays(30) : DateTime.UtcNow.AddMinutes(30);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity([
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault() ?? string.Empty)
                ]),
                Expires = tokenExpire,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescription);

            loginResponse.UserId = user.Id;
            loginResponse.Role = roles.FirstOrDefault() ?? string.Empty;
            loginResponse.Token = tokenHandler.WriteToken(token);
            loginResponse.TokenExpire = tokenExpire;

            _response.Success = true;
            _response.StatusCode = HttpStatusCode.OK;
            _response.Message = "Login successful.";
            _response.Result = loginResponse;
            return _response;
        }
        catch (Exception e)
        {
            _response.Success = false;
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.Message = e.Message;
            return _response;
        }
    }

    public async Task<ApiResponse> Register(ApplicationUser request, string role)
    {
        try
        {
            var isUserUnique = await IsUserUnique(request.UserName);
            if (!isUserUnique)
            {
                _response.Success = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Message = "User name is already in use.";
                return _response;
            }

            var result = await _userManager.CreateAsync(request, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(request, role);
                _response.Success = true;
                _response.StatusCode = HttpStatusCode.Created;
                _response.Message = "Registration successful.";
                return _response;
            }
            _response.Success = false;
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.Message = "Registration failed.";
            return _response;
            

        }catch(Exception e)
        {
            _response.Success = false;
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.Message = e.Message;
            return _response;
        }
    }

    public async Task<ApiResponse> UpdatePassword(UpdatePasswordDto request)
    {
        try
        {
            if (string.IsNullOrEmpty(request.Password))
            {
                _response.Success = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Message = "Password Field is empty.";
                return _response;
            }

            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                _response.Success = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Message = "User not found.";
                return _response;
            }
            user.Password = request.Password;
            var result = await _userManager.ChangePasswordAsync(user, request.Password, request.Password);
            if (result.Succeeded)
            {
                await _db.SaveChangesAsync();
                _response.Success = true;
                _response.StatusCode = HttpStatusCode.OK;
                _response.Message = "Password updated successful.";
                return _response;
            }
        
            _response.Success = false;
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.Message = "Password update failed.";
            return _response;
        }
        catch (Exception e)
        {
            _response.Success = false;
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.Message = e.Message;
            return _response;
        }
        
    }

    public void Update(ApplicationUser user)
    {
        _db.Update(user);
    }
}