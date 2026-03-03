using System.Net;
using Asp.Versioning;
using HospitalManagementSystem.Models.DatabaseEntity.Patient;
using HospitalManagementSystem.Models.DatabaseEntity.Patient.Dto;
using HospitalManagementSystem.Models.GenericModels;
using HospitalManagementSystem.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Api.Controllers
{
    [Route("api/v{version:apiVersion}/patients")]
    [ApiController]
    [ApiVersion("1.0")]
    public class PatientController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet("getall")]
        [Authorize(Roles =  "admin,patient")]
        public async Task<ApiResponse> GetAllPatient(CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                var genericReq = new GenericServiceRequest<Patient>
                {
                    NoTracking = false,
                    CancellationToken = cancellationToken

                };
                var data = await serviceManager.PatientService.GetAllAsync(genericReq);
                if (!data.Any())
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "No Patient found";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Successful";
                response.Result = data;
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
        [Authorize(Roles =  "admin,patient")]
        public async Task<ApiResponse> GetPatientById(Guid id,CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                var genericReq = new GenericServiceRequest<Patient>
                {
                    Expression = x=> x.Id == id,
                    NoTracking = true,
                    CancellationToken = cancellationToken

                };
                var data = await serviceManager.PatientService.GetAsync(genericReq);
                if (data == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "No Departments found";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Successful";
                response.Result = data;
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
        [Authorize(Roles =  "admin,patient")]
        public async Task<ApiResponse> GetPatientByCode(string code,CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                var genericReq = new GenericServiceRequest<Patient>
                {
                    Expression = x=> x.PatientCode == code,
                    NoTracking = true,
                    CancellationToken = cancellationToken

                };
                var data = await serviceManager.PatientService.GetAsync(genericReq);
                if (data == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "No Departments found";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Successful";
                response.Result = data;
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

        [HttpPost("create")]
        [Authorize(Roles = "admin,patient")]
        public async Task<ApiResponse> CreatePatient(CreatePatientDto request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                var existingData = await serviceManager.PatientService.GetAsync(new GenericServiceRequest<Patient>
                {
                    Expression = x=>x.PhoneNumber == request.PhoneNumber,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                });
                if (existingData is not null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Patient already exists with this phone-number.";
                    return response;
                }
                var patientCode = serviceManager.GeneratorCodeService.GpCodeAsync().Result;
                var reqToCreate = new Patient()
                {
                    PatientCode = patientCode,
                    FullName = request.FullName,
                    Gender =  request.Gender,
                    BloodGroup =  request.BloodGroup,
                    DateOfBirth =  request.DateOfBirth,
                    CreatedAt = DateTime.UtcNow,
                    Status = true
                };
                await serviceManager.PatientService.AddAsync(reqToCreate);
                var res = await serviceManager.Save();
                if (res > 0)
                {
                    response.Success = true;
                    response.StatusCode = HttpStatusCode.Created;
                    response.Message = "Created Successful.";
                    
                }
                else
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Creation Failed.";
                
                }
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
        
        [HttpPost("update")]
        [Authorize(Roles = "admin,patient")]
        public async Task<ApiResponse> UpdatePatient(UpdatePatientDto request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                if (String.IsNullOrEmpty(request.Id.ToString()))
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Invalid request Id";
                    return response;
                }

                var existingData = await serviceManager.PatientService.GetAsync(new GenericServiceRequest<Patient>
                {
                    Expression = x=>x.Id == request.Id,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                });
                if (existingData is null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Patient Not Found";
                    return response;
                }
                existingData.FullName = request.FullName;
                existingData.PhoneNumber = request.PhoneNumber;
                existingData.Gender = request.Gender;
                existingData.BloodGroup = request.BloodGroup;
                existingData.DateOfBirth = request.DateOfBirth;
                existingData.UpdatedAt = DateTime.UtcNow;
            
                serviceManager.PatientService.Update(existingData);
                var res = await serviceManager.Save();
                if (res > 0)
                {
                    response.Success = true;
                    response.StatusCode = HttpStatusCode.OK;
                    response.Message = "Updated Successful.";
                }
                else
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Update Failed.";
                }
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
        
        [HttpDelete("delete")]
        [Authorize(Roles =  "admin,department")]
        public async Task<ApiResponse> DeletePatient(Guid id,CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                if (String.IsNullOrEmpty(id.ToString()))
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Invalid request Id";
                    return response;
                }
                var genericReq = new GenericServiceRequest<Patient>
                {
                    Expression = x=> x.Id == id,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                };
                var data = await serviceManager.PatientService.GetAsync(genericReq);
                if (data == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "No patient found";
                    return response;
                }
                serviceManager.PatientService.Remove(data);
                await serviceManager.Save();
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Delete Successful";
                response.Result = data;
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
