using HospitalManagementSystem.Database.Data;
using HospitalManagementSystem.Models.DatabaseEntity.Doctor;
using HospitalManagementSystem.Services.IService;

namespace HospitalManagementSystem.Services.Service;

public class DoctorService(HMSDbContext db) : Service<Doctor>(db), IDoctorService
{
    public void Update(Doctor doctor)
    {
        db.Update(doctor);
    }
}