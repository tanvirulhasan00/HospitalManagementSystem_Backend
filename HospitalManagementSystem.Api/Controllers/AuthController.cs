using System.Net;
using System.Text.RegularExpressions;
using Asp.Versioning;
using HospitalManagementSystem.Models.DatabaseEntity.User;
using HospitalManagementSystem.Models.DatabaseEntity.User.Dto;
using HospitalManagementSystem.Models.GenericModels;
using HospitalManagementSystem.Services.IService;
using HospitalManagementSystem.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Api.Controllers
{
    [Route("api/v{version:apiVersion}/auth")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AuthController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public AuthController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost("login")]
        public async Task<ApiResponse> Login(LoginRequest request)
        {
            var response = await _serviceManager.AuthService.Login(request);
            return response;
        }
        
        
        [HttpPost("register")]
        [Consumes("multipart/form-data")]
        public async Task<ApiResponse> Registration([FromForm] CreateAppUserDto request)
        {
            var errorCatch = new ApiResponse();
            try
            {
                var stuffCode = await _serviceManager.GeneratorCodeService.GenerateCodeAsync(request.Role);
                var imageUrl = "";
                if (request.ImageUrl != null)
                {
                    imageUrl = await _serviceManager.File.FileUpload(request.ImageUrl,"images/user");
                }

                var username = Regex.Replace(request.FullName.ToLower(), @"\s+", "");
                //user model to create
                var newUser = new ApplicationUser()
                {
                    UserName = username,
                    FullName = request.FullName,
                    Address = request.Address,
                    StuffCode = stuffCode,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    Password = request.Password,
                    Designation = request.Designation,
                    LicenseNumber =  request.LicenseNumber,
                    NidNumber =  request.NidNumber,
                    PassportNumber =  request.PassportNumber,
                    DateOfBirth =  request.DateOfBirth,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt =  DateTime.UtcNow,
                    ImageUrl = imageUrl,
                    DepartmentId =  request.DepartmentId

                };
                var response = await _serviceManager.AuthService.Register(newUser,request.Role);
                
                return response;
            }
            catch (Exception e)
            {
                errorCatch.Success = false;
                errorCatch.StatusCode = HttpStatusCode.InternalServerError;
                errorCatch.Message = e.Message;
                return errorCatch;
            }
        }
    }
}
