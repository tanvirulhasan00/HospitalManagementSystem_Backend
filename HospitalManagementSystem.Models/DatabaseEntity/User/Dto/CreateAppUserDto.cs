using HospitalManagementSystem.Utilities;
using Microsoft.AspNetCore.Http;


namespace HospitalManagementSystem.Models.DatabaseEntity.User.Dto;

public class CreateAppUserDto
{
    public string FullName { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;
    public string Role { get; set; }

    public string Password { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    

    public string? Designation { get; set; } = string.Empty;

    public string? LicenseNumber { get; set; }  = string.Empty;

    public string? NidNumber { get; set; }   = string.Empty;

    public string? PassportNumber { get; set; }  = string.Empty;
    public DateOnly DateOfBirth { get; set; }

    public IFormFile? ImageUrl { get; set; }

    public Guid? DepartmentId { get; set; }
}