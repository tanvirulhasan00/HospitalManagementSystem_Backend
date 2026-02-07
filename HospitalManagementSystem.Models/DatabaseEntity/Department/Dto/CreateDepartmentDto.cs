using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models.DatabaseEntity.Department.Dto;

public class CreateDepartmentDto
{
    [MaxLength(50)]
    public string Name { get; set; }
}