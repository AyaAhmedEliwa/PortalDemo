using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portal.Core.Helper;
using Portal.Core.Interface;
using Portal.Core.Model;
using Portal.Infrastructure.Entities;

namespace Portal.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        #region prop

        private readonly IEmployeeRep employeeRep;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        #endregion

        #region ctor
        public EmployeeController(IEmployeeRep employeeRep, IMapper mapper, IConfiguration configuration)
        {
            this.employeeRep = employeeRep;
            this.mapper = mapper;
            this.configuration = configuration; //to see strings in appsetting.json -> "Response:Success:Code"
        }
        #endregion

        #region Actions

        [HttpGet, Route("~/api/GetEmployees")]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var emps = await employeeRep.GetAllAsync(emp => emp.IsActive == true && emp.IsDeleted == false);
                var data = mapper.Map<IEnumerable<EmployeeVM>>(emps);
                return Ok(new ApiResponse<IEnumerable<EmployeeVM>>()
                {
                    Code = configuration["Response:Success:Code"],
                    Status = configuration["Response:Success:Status"],
                    Success = data
                });
            }
            catch (Exception ex) 
            {
                return NotFound(new ApiResponse<string>()
                {
                    Code = configuration["Response:Erorr:Code"],
                    Status = configuration["Response:Erorr:Status"],
                    Erorr = ex.Message
                });
            }
        }

        //[HttpGet, Route("~/api/GetEmployeeById")]   quiery string
        [HttpGet, Route("~/api/GetEmployeeById/{id}")]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            try
            {
                var emp = await employeeRep.GetByIdAsync(emp => emp.Id == id && emp.IsActive == true && emp.IsDeleted == false);
                var data = mapper.Map<EmployeeVM>(emp);
                return Ok(new ApiResponse<EmployeeVM>()
                {
                    Code = configuration["Response:Success:Code"],
                    Status = configuration["Response:Success:Status"],
                    Success = data
                });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<string>()
                {
                    Code = configuration["Response:Erorr:Code"],
                    Status = configuration["Response:Erorr:Status"],
                    Erorr = ex.Message
                });
            }
        }

        [HttpPost, Route("~/api/PostEmployee")]
        public async Task<IActionResult> PostEmployee(EmployeeVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return NotFound("Validation Erorr");

                var data = mapper.Map<Employee>(model);
                await employeeRep.CreateAsync(data);

                return Ok(new ApiResponse<EmployeeVM>()
                {
                    Code = configuration["Response:Created:Code"],
                    Status = configuration["Response:Created:Status"]                    
                });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<string>()
                {
                    Code = configuration["Response:Erorr:Code"],
                    Status = configuration["Response:Erorr:Status"],
                    Erorr = ex.Message
                });
            }
        }


        [DisableCors]   //make the method disable for all servers
        [EnableCors("https://localhost:7109/")]     //make the method enable for this server only
        [HttpPut, Route("~/api/UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(EmployeeVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return NotFound("Validation Erorr");

                var data = mapper.Map<Employee>(model);
                await employeeRep.UpdateAsync(data);

                return Ok(new ApiResponse<Employee>()
                {
                    Code = configuration["Response:Updated:Code"],
                    Status = configuration["Response:Updated:Status"],
                    Success = data
                }); 
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<string>()
                {
                    Code = configuration["Response:Erorr:Code"],
                    Status = configuration["Response:Erorr:Status"],
                    Erorr = ex.Message
                });
            }
        }

        [HttpDelete, Route("~/api/DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee(EmployeeVM model)
        {
            try
            {
                
                var data = mapper.Map<Employee>(model);
                await employeeRep.DeleteAsync(data);

                return Ok(new ApiResponse<EmployeeVM>()
                {
                    Code = configuration["Response:Deleted:Code"],
                    Status = configuration["Response:Deleted:Status"]
                });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<string>()
                {
                    Code = configuration["Response:Erorr:Code"],
                    Status = configuration["Response:Erorr:Status"],
                    Erorr = ex.Message
                });
            }
        }

        #endregion

    }
}
