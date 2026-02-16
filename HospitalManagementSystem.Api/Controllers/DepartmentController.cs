using System.Net;
using Asp.Versioning;
using HospitalManagementSystem.Models.DatabaseEntity.Department;
using HospitalManagementSystem.Models.DatabaseEntity.Department.Dto;
using HospitalManagementSystem.Models.GenericModels;
using HospitalManagementSystem.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Api.Controllers
{
    [Route("api/v{version:apiVersion}/departments")]
    [ApiController]
    [ApiVersion("1.0")]
    public class DepartmentController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet("getall")]
        [Authorize(Roles =  "admin,department")]
        public async Task<ApiResponse> GetAllDepartment(CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                var genericReq = new GenericServiceRequest<Department>
                {
                    NoTracking = false,
                    CancellationToken = cancellationToken

                };
                var deptData = await serviceManager.DepartmentService.GetAllAsync(genericReq);
                if (!deptData.Any())
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "No Departments found";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Successful";
                response.Result = deptData;
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
        [Authorize(Roles =  "admin,department")]
        public async Task<ApiResponse> GetDepartmentById(Guid id,CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                var genericReq = new GenericServiceRequest<Department>
                {
                    Expression = x=> x.Id == id,
                    NoTracking = true,
                    CancellationToken = cancellationToken

                };
                var deptData = await serviceManager.DepartmentService.GetAsync(genericReq);
                if (deptData == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "No Departments found";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Successful";
                response.Result = deptData;
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
        [Authorize(Roles =  "admin,department")]
        public async Task<ApiResponse> GetDepartmentByCode(string deptCode,CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                var genericReq = new GenericServiceRequest<Department>
                {
                    Expression = x=> x.DepartmentCode == deptCode,
                    NoTracking = true,
                    CancellationToken = cancellationToken

                };
                var deptData = await serviceManager.DepartmentService.GetAsync(genericReq);
                if (deptData == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "No Departments found";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Successful";
                response.Result = deptData;
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
        [Authorize(Roles = "admin,department")]
        public async Task<ApiResponse> CreateDepartment(CreateDepartmentDto request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                if (request == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Invalid request";
                    return response;
                }

                var existingDept = await serviceManager.DepartmentService.GetAsync(new GenericServiceRequest<Department>
                {
                    Expression = x=>x.Name.ToLower() == request.Name.ToLower(),
                    NoTracking = true,
                    CancellationToken = cancellationToken
                });
                if (existingDept is not null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Department already exists with this name.";
                    return response;
                }
                var deptCode = serviceManager.GeneratorCodeService.GdCodeAsync(request.Name).Result;
                var reqToCreate = new Department()
                {
                    DepartmentCode =  deptCode,
                    Name =  request.Name,
                    CreateAt = DateTime.UtcNow,
                    Status = true
                };
                await serviceManager.DepartmentService.AddAsync(reqToCreate);
                var res = await serviceManager.Save();
                if (res > 0)
                {
                    response.Success = true;
                    response.StatusCode = HttpStatusCode.Created;
                    response.Message = "Created Successful.";
                    return response;
                }
                else
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Creation Failed.";
                    return response;
                
                }
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
        [Authorize(Roles = "admin,department")]
        public async Task<ApiResponse> UpdateDepartment(UpdateDepartmentDto request, CancellationToken cancellationToken)
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

                var existingDept = await serviceManager.DepartmentService.GetAsync(new GenericServiceRequest<Department>
                {
                    Expression = x=>x.Id == request.Id,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                });
                if (existingDept is null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Department Not Found";
                    return response;
                }
                existingDept.Name = request.Name;
                existingDept.UpdateAt = DateTime.UtcNow;
            
                serviceManager.DepartmentService.Update(existingDept);
                var res = await serviceManager.Save();
                if (res > 0)
                {
                    response.Success = true;
                    response.StatusCode = HttpStatusCode.Created;
                    response.Message = "Updated Successful.";
                    return response;
                }
                else
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Update Failed.";
                    return response;
                
                }
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
        
        [HttpGet("delete")]
        [Authorize(Roles =  "admin,department")]
        public async Task<ApiResponse> DeleteDepartment(Guid id,CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                var genericReq = new GenericServiceRequest<Department>
                {
                    Expression = x=> x.Id == id,
                    NoTracking = true,
                    CancellationToken = cancellationToken

                };
                var deptData = await serviceManager.DepartmentService.GetAsync(genericReq);
                if (deptData == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "No Departments found";
                    return response;
                }
                serviceManager.DepartmentService.Remove(deptData);
                await serviceManager.Save();
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Delete Successful";
                response.Result = deptData;
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
