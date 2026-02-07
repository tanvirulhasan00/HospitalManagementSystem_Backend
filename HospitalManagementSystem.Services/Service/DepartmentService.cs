using HospitalManagementSystem.Database.Data;
using HospitalManagementSystem.Models.DatabaseEntity.Department;
using HospitalManagementSystem.Services.IService;

namespace HospitalManagementSystem.Services.Service;

public class DepartmentService(HMSDbContext db) : Service<Department>(db), IDepartmentService
{
    public void Update(Department department)
    {
        db.Update(department);
    }
}