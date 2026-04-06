namespace HospitalManagementSystem.Models.DatabaseEntity.Doctor.Dto;

public class DisplayDoctorData
{
    public string StuffCode { get; init; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? Designation { get; set; } = string.Empty;
    public string? LicenseNumber { get; set; }  = string.Empty;
    public string? NidNumber { get; set; }   = string.Empty;
    public string? PassportNumber { get; set; }  = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public DateTime CreatedAt { get; init; } 

    public string? ImageUrl { get; set; }  = string.Empty;


    public string? DepartmentName { get; set; }
    
}