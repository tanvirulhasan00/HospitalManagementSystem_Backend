namespace HospitalManagementSystem.Models.DatabaseEntity.Appointment.Dto;

public class CreateAppointmentDto
{
    public string PatientCode { get; set; }  = string.Empty;
    public string? DepartmentName { get; set; } = string.Empty;
    public string DoctorName { get; set; }  = string.Empty;
    public DateTime AppointDate { get; set; }
    public int? SerialNo { get; set; }
    public string? Problem { get; set; }  = string.Empty;
}