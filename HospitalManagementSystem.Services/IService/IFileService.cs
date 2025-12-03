using Microsoft.AspNetCore.Http;

namespace HospitalManagementSystem.Services.IService;

public interface IFileService
{
    Task<string> FileUpload(IFormFile file, string folderName);
    void DeleteFile(string fileUrl);
}