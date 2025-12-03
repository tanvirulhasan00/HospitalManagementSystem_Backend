using HospitalManagementSystem.Services.IService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace HospitalManagementSystem.Services.Service;

public class FileService : IFileService
{
    private readonly IWebHostEnvironment _env;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public FileService(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
    {
        _env = env;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> FileUpload(IFormFile file, string folderName)
    {
        // Get the root path of wwwroot
        var rootPath = _env.WebRootPath;

        // Generate unique names for the files
        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

        // Combine root path with file names to create file paths
        var filePath = Path.Combine(rootPath, folderName, fileName);

        // Ensure the "images" folder exists in wwwroot
        var folder = Path.Combine(rootPath, folderName);
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        // Save the profile picture
        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Create URLs for the saved files
        var fileUrl = $"{_httpContextAccessor.HttpContext?.Request.Scheme}://{_httpContextAccessor.HttpContext?.Request.Host}/{folderName}/{fileName}";
        return fileUrl;
    }

    public void DeleteFile(string fileUrl)
    {
        // Get the root path of wwwroot
        var rootPath = _env.WebRootPath;
        var uri = new Uri(fileUrl);
        var relativePath = uri.AbsolutePath.TrimStart('/');
        // Delete old picture if it exists
        if (!string.IsNullOrEmpty(fileUrl))
        {
            var filePath = Path.Combine(rootPath, relativePath);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}