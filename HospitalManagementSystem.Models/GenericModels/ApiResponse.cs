using System.Net;

namespace HospitalManagementSystem.Models.GenericModels;

public class ApiResponse
{
    public HttpStatusCode StatusCode { get; set; }
    public bool Success { get; set; }
    public string? Message { get; set; }
    public object? Result { get; set; }
}