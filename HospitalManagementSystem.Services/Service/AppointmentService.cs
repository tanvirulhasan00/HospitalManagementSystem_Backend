using HospitalManagementSystem.Database.Data;
using HospitalManagementSystem.Models.DatabaseEntity.Appointment;
using HospitalManagementSystem.Services.IService;

namespace HospitalManagementSystem.Services.Service;

public class AppointmentService(HMSDbContext db) : Service<Appointment>(db), IAppointmentService
{
    public void Update(Appointment appointment)
    {
        db.Update(appointment);
    }
}