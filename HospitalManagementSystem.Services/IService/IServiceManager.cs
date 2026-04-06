namespace HospitalManagementSystem.Services.IService;

public interface IServiceManager
{
    Task<int> Save();

    // service registration
    public IAuthService AuthService { get; }
    public IDepartmentService DepartmentService { get; }
    public IPatientService PatientService { get; }
    public IDoctorService DoctorService { get; }
    public ICodeGeneratorService GeneratorCodeService { get; }
    public IFileService File { get; }
}