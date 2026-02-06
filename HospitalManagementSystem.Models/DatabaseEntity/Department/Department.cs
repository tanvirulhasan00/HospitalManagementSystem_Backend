using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models.DatabaseEntity.Department;

public class Department
{
    [Key]
    public Guid Id { get; init; }

    [MaxLength(100)] 
    public string DepartmentCode { get; init; }
    [MaxLength(50)]
    public string Name { get; set; } 

    public DateTime CreateAt { get; init; } 
    public DateTime UpdateAt { get; set; } 
    public bool Status { get; set; }  = true;
}