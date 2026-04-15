using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models.DatabaseEntity.Appointment;

public class Appointment
{
    public Guid Id { get; init; }
    [Required]
    public string AppointmentCode { get; init; } = string.Empty;
    [Required]
    public string PatientCode { get; set; }  = string.Empty;
    public string? DepartmentName { get; set; } = string.Empty;
    public string DoctorName { get; set; }  = string.Empty;
    public DateTime AppointDate { get; set; }
    public int? SerialNo { get; set; }
    public string? Problem { get; set; }  = string.Empty;
    public bool Status { get; set; }
}