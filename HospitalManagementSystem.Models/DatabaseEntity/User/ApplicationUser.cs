using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace HospitalManagementSystem.Models.DatabaseEntity.User;

public class ApplicationUser : IdentityUser
{
    [MaxLength(100)]
    public string StuffCode { get; set; } = string.Empty;
    [MaxLength(20)]
    public string Password { get; set; } = string.Empty;
    [MaxLength(20)]
    public string FullName { get; set; } = string.Empty;
    [MaxLength(100)]
    public string Address { get; set; } = string.Empty;
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
    public DateTime UpdatedAt { get; set; }
    public string? ImageUrl { get; set; }  = string.Empty;
    
    //soft delete
    public bool IsDeleted { get; set; } = false; // "active = true,inactive = false"
    public DateTime DeletedAt { get; set; }


    public Guid? DepartmentId { get; set; }
    [ForeignKey(nameof(DepartmentId))] 
    public Department.Department? Department { get; set; }
    
    
    
    
}