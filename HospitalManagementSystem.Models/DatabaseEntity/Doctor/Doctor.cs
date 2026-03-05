using HospitalManagementSystem.Models.DatabaseEntity.User;

namespace HospitalManagementSystem.Models.DatabaseEntity.Doctor;

public class Doctor : ApplicationUser
{
    public string Designation { get; set; }
    public string LicenseNumber { get; set; }  = string.Empty;
}