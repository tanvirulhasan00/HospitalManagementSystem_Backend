using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models.DatabaseEntity.Patient.Dto;

public class UpdatePatientDto
{
    public Guid Id { get; init; }
    [MaxLength(50)]
    public string FullName { get; set; } = string.Empty;

    [Required] 
    [MaxLength(20)] 
    public string PhoneNumber { get; set; } = string.Empty;
    
    [MaxLength(20)] 
    public string? Gender { get; set; }
    [MaxLength(20)]
    public string? BloodGroup { get; set; }
    public DateTime DateOfBirth { get; set; }

}