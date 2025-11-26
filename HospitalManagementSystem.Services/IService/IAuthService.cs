using HospitalManagementSystem.Models.DatabaseEntity.User;
using HospitalManagementSystem.Models.DatabaseEntity.User.Dto;
using HospitalManagementSystem.Models.GenericModels;
using Microsoft.AspNetCore.Identity.Data;
using LoginRequest = HospitalManagementSystem.Models.GenericModels.LoginRequest;

namespace HospitalManagementSystem.Services.IService;

public interface IAuthService : IService<ApplicationUser>
{
    Task<bool> IsPhoneNumberUnique(string phoneNumber);
    Task<bool> IsEmailUnique(string email);
    Task<bool> IsUserUnique(string userName);
    Task<ApiResponse> Login(LoginRequest request);
    Task<ApiResponse> Register(CreateAppUserDto request);
    void Update(ApplicationUser user);
}