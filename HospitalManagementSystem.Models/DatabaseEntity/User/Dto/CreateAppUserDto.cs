using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace HospitalManagementSystem.Models.DatabaseEntity.User.Dto;

public class CreateAppUserDto
{
    [MaxLength(20)]
    public string FullName { get; set; } = string.Empty;
    [MaxLength(20)]
    public string Password { get; set; } = string.Empty;
    [MaxLength(20)]
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    
    [MaxLength(20)]
    public string? Designation { get; set; } = string.Empty;
    [MaxLength(20)] 
    public string? LicenseNumber { get; set; }  = string.Empty;
    [MaxLength(20)] 
    public string? NidNumber { get; set; }   = string.Empty;
    [MaxLength(20)]
    public string? PassportNumber { get; set; }  = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public DateTime CreatedAt { get; set; } 
    public IFormFile? ImageUrl { get; set; }

    public Guid? DepartmentId { get; set; }
}