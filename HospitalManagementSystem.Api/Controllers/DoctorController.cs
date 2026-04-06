using System.Net;
using Asp.Versioning;
using HospitalManagementSystem.Models.DatabaseEntity.Doctor;
using HospitalManagementSystem.Models.DatabaseEntity.Doctor.Dto;
using HospitalManagementSystem.Models.GenericModels;
using HospitalManagementSystem.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Api.Controllers
{
    [Route("api/v{version:apiVersion}/patients")]
    [ApiController]
    [ApiVersion("1.0")]
    public class DoctorController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet("getall")]
        [Authorize(Roles =  "admin,doctor")]
        public async Task<ApiResponse> GetAllDoctor(CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                var genericReq = new GenericServiceRequest<Doctor>
                {
                    NoTracking = false,
                    CancellationToken = cancellationToken

                };
                var data = await serviceManager.DoctorService.GetAllAsync(genericReq);
                if (!data.Any())
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "No Doctors found";
                    return response;
                }

                var doctorData = data.Select(d => new DisplayDoctorDataList
                {
                    FullName = d.FullName,
                    StuffCode = d.StuffCode,
                    Designation = d.Designation,
                    ImageUrl =   d.ImageUrl,
                });
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Successful";
                response.Result = doctorData;
                return response;
            }
            catch (TaskCanceledException ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.RequestTimeout;
                response.Message = ex.Message;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                return response;
            }
            
            
        }
        
        [HttpGet("get-by-id")]
        [Authorize(Roles =  "admin,doctor")]
        public async Task<ApiResponse> GetDoctorById(string id,CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                var genericReq = new GenericServiceRequest<Doctor>
                {
                    Expression = x=> x.Id == id,
                    NoTracking = true,
                    CancellationToken = cancellationToken

                };
                var data = await serviceManager.DoctorService.GetAsync(genericReq);
                if (data == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "No Doctor found";
                    return response;
                }
                var doctorData = new DisplayDoctorData
                {
                    StuffCode = data.StuffCode,
                    FullName = data.FullName,
                    Address = data.Address,
                    Designation = data.Designation,
                    LicenseNumber =  data.LicenseNumber,
                    NidNumber =  data.NidNumber,
                    PassportNumber = data.PassportNumber,
                    DateOfBirth = data.DateOfBirth,
                    CreatedAt =  data.CreatedAt,
                    DepartmentName = data.Department?.Name,
                    ImageUrl =   data.ImageUrl,
                };
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Successful";
                response.Result = doctorData;
                return response;
            }
            catch (TaskCanceledException ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.RequestTimeout;
                response.Message = ex.Message;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                return response;
            }
            
            
        }
        
        [HttpGet("get-by-code")]
        [Authorize(Roles =  "admin,doctor")]
        public async Task<ApiResponse> GetDoctorByCode(string code,CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                var genericReq = new GenericServiceRequest<Doctor>
                {
                    Expression = x=> x.StuffCode == code,
                    NoTracking = true,
                    CancellationToken = cancellationToken

                };
                var data = await serviceManager.DoctorService.GetAsync(genericReq);
                if (data == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "No Doctor found";
                    return response;
                }
                var doctorData = new DisplayDoctorData
                {
                    StuffCode = data.StuffCode,
                    FullName = data.FullName,
                    Address = data.Address,
                    Designation = data.Designation,
                    LicenseNumber =  data.LicenseNumber,
                    NidNumber =  data.NidNumber,
                    PassportNumber = data.PassportNumber,
                    DateOfBirth = data.DateOfBirth,
                    CreatedAt =  data.CreatedAt,
                    DepartmentName = data.Department?.Name,
                    ImageUrl =   data.ImageUrl,
                };
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Successful";
                response.Result = doctorData;
                return response;
            }
            catch (TaskCanceledException ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.RequestTimeout;
                response.Message = ex.Message;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                return response;
            }
            
            
        }

    }
}
