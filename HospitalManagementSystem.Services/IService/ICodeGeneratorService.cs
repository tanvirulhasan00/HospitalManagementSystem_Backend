using HospitalManagementSystem.Utilities;

namespace HospitalManagementSystem.Services.IService;

public interface ICodeGeneratorService
{
    Task<string> GenerateCodeAsync(string role);
    Task<string> GdCodeAsync(string name); //Generate Department Code
    Task<string> GpCodeAsync(); //Generate Patient Code
}