using System.Net;
using Asp.Versioning;
using HospitalManagementSystem.Models.DatabaseEntity.Appointment;
using HospitalManagementSystem.Models.GenericModels;
using HospitalManagementSystem.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Api.Controllers
{
    [Route("api/v{version:apiVersion}/patients")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AppointmentController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet("getall")]
        [Authorize(Roles =  "admin")]
        public async Task<ApiResponse> GetAllAppointment(CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                var genericReq = new GenericServiceRequest<Appointment>
                {
                    NoTracking = false,
                    CancellationToken = cancellationToken

                };
                var data = await serviceManager.AppointmentService.GetAllAsync(genericReq);
                if (!data.Any())
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "No Appointments found";
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
        [Authorize(Roles =  "admin")]
        public async Task<ApiResponse> GetAppointmentById(string id,CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                var genericReq = new GenericServiceRequest<Appointment>
                {
                    Expression = x=> x.Id.ToString() == id,
                    NoTracking = true,
                    CancellationToken = cancellationToken

                };
                var data = await serviceManager.AppointmentService.GetAsync(genericReq);
                if (data == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "No Appointments found";
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
        [Authorize(Roles =  "admin")]
        public async Task<ApiResponse> GetAppointmentByCode(string code,CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                var genericReq = new GenericServiceRequest<Appointment>
                {
                    Expression = x=> x.AppointmentCode == code,
                    NoTracking = true,
                    CancellationToken = cancellationToken

                };
                var data = await serviceManager.AppointmentService.GetAsync(genericReq);
                if (data == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "No Appointments found";
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
        
        [HttpGet("get-by-patient-code")]
        [Authorize(Roles =  "admin")]
        public async Task<ApiResponse> GetAppointmentByPatientCode(string code,CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                var genericReq = new GenericServiceRequest<Appointment>
                {
                    Expression = x=> x.PatientCode == code,
                    NoTracking = true,
                    CancellationToken = cancellationToken

                };
                var data = await serviceManager.AppointmentService.GetAsync(genericReq);
                if (data == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "No Appointments found";
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

    }
}
