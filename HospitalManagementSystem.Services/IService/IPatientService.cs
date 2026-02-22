using HospitalManagementSystem.Models.DatabaseEntity.Department;
using HospitalManagementSystem.Models.DatabaseEntity.Patient;

namespace HospitalManagementSystem.Services.IService;

public interface IPatientService : IService<Patient>
{
    void Update(Patient patient);
}