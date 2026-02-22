using HospitalManagementSystem.Database.Data;
using HospitalManagementSystem.Models.DatabaseEntity.Patient;
using HospitalManagementSystem.Services.IService;

namespace HospitalManagementSystem.Services.Service;

public class PatientService(HMSDbContext db) : Service<Patient>(db), IPatientService
{
    public void Update(Patient patient)
    {
        db.Update(patient);
    }
}