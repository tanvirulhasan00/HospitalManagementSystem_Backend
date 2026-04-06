namespace HospitalManagementSystem.Models.DatabaseEntity.Doctor.Dto;

public class DisplayDoctorDataList
{
    public string StuffCode { get; init; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string? Designation { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }  = string.Empty;
    
}