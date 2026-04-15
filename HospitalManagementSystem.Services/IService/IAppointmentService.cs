using HospitalManagementSystem.Models.DatabaseEntity.Appointment;

namespace HospitalManagementSystem.Services.IService;

public interface IAppointmentService : IService<Appointment>
{
    void Update(Appointment appointment);
}