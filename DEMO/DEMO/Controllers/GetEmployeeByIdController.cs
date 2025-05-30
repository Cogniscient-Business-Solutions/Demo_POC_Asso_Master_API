using System.Collections;
using DEMO.Models.DataDL.Classes;
using DEMO.Models.BusinessDL;
using DEMO.Models.DTO.EmpDetail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using static DEMO.Models.BusinessDL.GetEmployeeByIdService;
using Swashbuckle.AspNetCore.Filters;
using DEMO.SwaggerExamples;


namespace DEMO.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class GetEmployeeByIdController : ControllerBase
    {
        
        private readonly GetEmployeeByIdService _GetEmployeeByIdService;
        private readonly Hashtable objht = new Hashtable();
        private readonly ILogger<GetEmployeeByIdController> _logger;
        private readonly TokenService _tokenService;

        public GetEmployeeByIdController( GetEmployeeByIdService GetEmployeeByIdService, ILogger<GetEmployeeByIdController> logger, TokenService tokenService) // Inject logger
        {
            
            _GetEmployeeByIdService = GetEmployeeByIdService;
            _logger = logger;
            _tokenService = tokenService;
        }


        /// <summary>
        /// IN THIS API WE ARE RETRIEVING ASSOCIATES DATA 
        /// </summary>
        /// <param name="ASSO_CODE">The employee association code.</param>
        /// <returns>Returns employee details if found, otherwise an error response.</returns>
        /// <response code="200">Employee details retrieved successfully.</response>
        /// <response code="401">Unauthorized - Invalid or missing JWT token.</response>
        /// <response code="404">Employee not found.</response>
        /// <response code="500">Internal server error.</response>
        [Produces("application/json")]
        [HttpGet("EmpDetail")]
        public async Task<IActionResult> EmployeeDetail(string ASSO_CODE)
        {
            try
            {
                // Extract token and decode claims
                var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var claims = _tokenService.DecodeToken(token);

                if (claims == null)
                {
                    return ApiResponseHelper.AuthErrorResponse("401", "Your session has expired. Please log in again.");
                }

                //// Assign company and location from claims if not provided
                //COMPANY_NO ??= claims.GetValueOrDefault("company");
                //LOCATION_NO ??= claims.GetValueOrDefault("location");

                //if (string.IsNullOrWhiteSpace(COMPANY_NO) || string.IsNullOrWhiteSpace(LOCATION_NO))
                //{
                //    return ApiResponseHelper.ErrorResponse("BAD_REQUEST", "Company and Location are required.");
                //}

                // Prepare parameters for the service call
                objht.Clear();
                objht["ASSO_CODE"] = ASSO_CODE ?? "";
                //objht["COMPANY_NO"] = COMPANY_NO;
                //objht["LOCATION_NO"] = LOCATION_NO;

                _GetEmployeeByIdService.ht = objht;

                // Call the service to fetch employee details
                var response = await _GetEmployeeByIdService.GetEmpDetailAsync();


                return response;
                // If response is already an API response, return it directly
                //if (response is ObjectResult objectResult)
                //{
                //    return objectResult;
                //}

                //return ApiResponseHelper.ErrorResponse("SERVER_ERROR", "Unexpected response format.");
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.ErrorResponse("SERVER_ERROR", "An unexpected error occurred.", ex.Message);
            }
        }


      

        ///// <summary>
        ///// Add Employee Resource.
        ///// </summary>
        ///// <remarks>
        ///// You can send different payloads for adding employees.
        ///// </remarks>
        ///// <param name="model">Employee request model</param>
        ///// <returns>Success or error message</returns>
        //[HttpPost("AddEmployee")]
        //[SwaggerRequestExample(typeof(EmployeeRequestModel), typeof(EmployeeMultipleExamples))]
        //public IActionResult AddEmployee([FromBody] EmployeeRequestModel model)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(model.ASSO_CODE) || string.IsNullOrEmpty(model.COMPANY_NO) || string.IsNullOrEmpty(model.LOCATION_NO))
        //        {
        //            return BadRequest(new { Error = "Company, Location, and Employee Code are required." });
        //        }

        //        model.EmployeeId = EmployeeDataStore.Employees.Count + 1;
        //        EmployeeDataStore.Employees.Add(model);

        //        return Ok(new { Message = "Employee added successfully!", EmployeeId = model.EmployeeId });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { Error = "An unexpected error occurred.", Details = ex.Message });
        //    }
        //}

        ///// <summary>
        ///// get emp details.
        ///// </summary>
        //[HttpGet("GetEmployees")]
        //public IActionResult GetEmployees()
        //{
        //    return ApiResponseHelper.SuccessResponse(new { Employees = EmployeeDataStore.Employees });
        //}

        ///// <summary>
        ///// get emp details by id.
        ///// </summary>
        //[HttpGet("EmpDetailById/{employeeId}")]
        //public IActionResult EmployeeDetailById(int employeeId)
        //{
        //    try
        //    {
        //        var employee = EmployeeDataStore.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);

        //        if (employee != null)
        //        {
        //            return ApiResponseHelper.SuccessResponse(new { EDetails = employee });
        //        }

        //        return ApiResponseHelper.ErrorResponse("USER_NOT_FOUND", "Employee not found.", "Please check the Employee ID and try again.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return ApiResponseHelper.ErrorResponse("SERVER_ERROR", "An unexpected error occurred.", ex.Message);
        //    }
        //}

        //[HttpPut("UpdateEmployee/{id}")]
        //public IActionResult UpdateEmployee(int id, [FromBody] EmployeeRequestModel updatedEmployee)
        //{
        //    var employee = EmployeeDataStore.Employees.FirstOrDefault(e => e.EmployeeId == id);
        //    if (employee == null)
        //        return NotFound(new { Message = "Employee not found" });

            
        //    employee.ASSO_CODE = updatedEmployee.ASSO_CODE;
        //    employee.COMPANY_NO = updatedEmployee.COMPANY_NO;
        //    employee.LOCATION_NO = updatedEmployee.LOCATION_NO;
        //    employee.EMP_NAME = updatedEmployee.EMP_NAME;
        //    employee.EMP_EMAIL = updatedEmployee.EMP_EMAIL;
        //    employee.EMP_PHONE = updatedEmployee.EMP_PHONE;

        //    return Ok(employee);
        //}

        
        //[HttpDelete("DeleteEmployee/{id}")]
        //public IActionResult DeleteEmployee(int id)
        //{
        //    var employee = EmployeeDataStore.Employees.FirstOrDefault(e => e.EmployeeId == id);
        //    if (employee == null)
        //        return NotFound(new { Message = "Employee not found" });

        //    EmployeeDataStore.Employees.Remove(employee);
        //    return Ok(new { Message = "Employee deleted successfully" });
        //}


    }
}
