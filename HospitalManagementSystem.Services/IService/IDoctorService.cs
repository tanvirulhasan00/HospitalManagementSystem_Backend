using HospitalManagementSystem.Models.DatabaseEntity.Doctor;

namespace HospitalManagementSystem.Services.IService;

public interface IDoctorService : IService<Doctor>
{
    void Update(Doctor doctor);
}