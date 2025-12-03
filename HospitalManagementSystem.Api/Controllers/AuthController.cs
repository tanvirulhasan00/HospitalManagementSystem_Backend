using Asp.Versioning;
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
        
        [HttpPost("get")]
        public async Task<ApiResponse> GetCode()
        {
            var stuffCodeG = await _serviceManager.GeneratorCodeService.GenerateCodeAsync(Role.Admin);
            Console.WriteLine("from db initializer" + stuffCodeG);
            var response = new ApiResponse();
            response.Result = stuffCodeG;
            return response;
        }
        [HttpPost("register")]
        public async Task<ApiResponse> Registration([FromForm]CreateAppUserDto request)
        {
            var response = await _serviceManager.AuthService.Register(request);
            return response;
        }

        [HttpPost("login")]
        public async Task<ApiResponse> Login(LoginRequest request)
        {
            var response = await _serviceManager.AuthService.Login(request);
            return response;
        }


    }
}
