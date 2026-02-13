using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models.DatabaseEntity.Department.Dto;

public class UpdateDepartmentDto
{
    public Guid Id { get; init; }
    [MaxLength(50)]
    public string Name { get ; set; } 
}