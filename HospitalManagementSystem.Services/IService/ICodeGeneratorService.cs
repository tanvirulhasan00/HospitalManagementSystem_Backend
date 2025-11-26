using HospitalManagementSystem.Utilities;

namespace HospitalManagementSystem.Services.IService;

public interface ICodeGeneratorService
{
    Task<string> GenerateCodeAsync(Role role);
}