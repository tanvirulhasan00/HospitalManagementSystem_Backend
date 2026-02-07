using HospitalManagementSystem.Models.DatabaseEntity.Department;

namespace HospitalManagementSystem.Services.IService;

public interface IDepartmentService : IService<Department>
{
    void Update(Department department);
}