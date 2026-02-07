using System.Net;
using Asp.Versioning;
using HospitalManagementSystem.Models.DatabaseEntity.Department;
using HospitalManagementSystem.Models.GenericModels;
using HospitalManagementSystem.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Api.Controllers
{
    [Route("api/v{version:apiVersion}/departments")]
    [ApiController]
    [ApiVersion("1.0")]
    public class DepartmentController(IServiceManager serviceManager, ApiResponse response) : ControllerBase
    {
        [HttpGet("getall")]
        [Authorize(Roles =  "admin,department")]
        public async Task<ApiResponse> GetAll(CancellationToken cancellationToken)
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
    }
}
