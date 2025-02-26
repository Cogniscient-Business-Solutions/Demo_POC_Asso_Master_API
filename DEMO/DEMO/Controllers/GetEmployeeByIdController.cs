using System.Collections;
using DEMO.Models.DataDL.Classes;
using DEMO.Models.BusinessDL;
using DEMO.Models.DTO.EmpDetail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DEMO.Controllers
{
    [Authorize]
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
        /// Retrieves employee details based on the given parameters.
        /// </summary>
        /// <param name="ASSO_CODE">The employee association code.</param>
        /// <param name="COMPANY_NO">The company number.</param>
        /// <param name="LOCATION_NO">The location number.</param>
        /// <returns>Returns employee details if found, otherwise an error response.</returns>
        /// <response code="200">Employee details retrieved successfully.</response>
        /// <response code="401">Unauthorized - Invalid or missing JWT token.</response>
        /// <response code="404">Employee not found.</response>
        /// <response code="500">Internal server error.</response>
        
        //[Produces("application/json")]
        [HttpGet("EmpDetail")]
        public async Task<IActionResult> EmployeeDetail(string ASSO_CODE, string COMPANY_NO = null, string LOCATION_NO = null)
        {
            try
            {

                var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var claims = _tokenService.DecodeToken(token);

                if (claims == null)
                {
                    return ApiResponseHelper.ErrorResponse("UNAUTHORIZED", "Invalid token.");
                }


                COMPANY_NO ??= claims.GetValueOrDefault("company");
                LOCATION_NO ??= claims.GetValueOrDefault("location");



                if (string.IsNullOrEmpty(COMPANY_NO) || string.IsNullOrEmpty(LOCATION_NO))
                {
                    return ApiResponseHelper.ErrorResponse("BAD_REQUEST", "Company and Location are required.");
                }

                objht.Clear();
                objht.Add("ASSO_CODE", ASSO_CODE ?? "");
                objht.Add("COMPANY_NO", COMPANY_NO ?? "");
                objht.Add("LOCATION_NO", LOCATION_NO ?? "");

                _GetEmployeeByIdService.ht = objht;

                var response = await _GetEmployeeByIdService.GetEmpDetailAsync();

                if (response.EmpMessage.Success)
                {
                    return ApiResponseHelper.SuccessResponse(new { EDetails = response.EDetails });
                }
                else
                {
                    return ApiResponseHelper.ErrorResponse(
                "USER_NOT_FOUND",
                response.EmpMessage.ErrorMsg,
                "Please check the employee ID and try again.");
                }
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.ErrorResponse(
            "SERVER_ERROR",
            "An unexpected error occurred.",
            ex.Message);
            }
        }

    }
}
