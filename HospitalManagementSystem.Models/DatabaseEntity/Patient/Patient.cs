using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models.DatabaseEntity.Patient;

public class Patient
{
    [Key]
    public Guid Id { get; set; }
    public string PatientCode { get; set; } = string.Empty;
    [MaxLength(50)]
    public string FullName { get; set; } = string.Empty;

    [Required] 
    [MaxLength(20)] 
    public string PhoneNumber { get; set; } = string.Empty;

    public string? Gender { get; set; }
    public string? BloodGroup { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool Status { get; set; }
}