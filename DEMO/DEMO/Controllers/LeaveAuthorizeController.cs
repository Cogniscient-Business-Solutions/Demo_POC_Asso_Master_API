using DEMO.Models.BusinessDL.Interfaces;
using DEMO.Models.DataDL.Classes;
using DEMO.Models.DTO.ApplyLeave;
using DEMO.Models.Generic;
using DEMO.SwaggerExamples;
using Microsoft.AspNetCore.Authorization;
using System.Collections;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using DEMO.Models.DTO.LeaveAuthorizeCancel;

namespace DEMO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveAuthorizeController : ControllerBase
    {
        private readonly Hashtable objht = new Hashtable();
        private readonly TokenService _tokenService;

        private readonly ILeaveService _leaveService;


        public LeaveAuthorizeController(TokenService tokenService, ILeaveService leaveService)
        {
            _tokenService = tokenService;

            _leaveService = leaveService;
        }


        /// <summary>
        /// THIS API IS USED TO AUTHORIZE THE LEAVE  
        /// </summary>
        [Authorize]
        [HttpPost("LeaveAuthorize")]
        [SwaggerRequestExample(typeof(LeaveRequest), typeof(LeaveAuthorizeExamples))]
        public async Task<IActionResult> LeaveAuthorize([FromBody] LeaveRequest request)
        {
            try
            {
                if (request == null)
                {
                    return ApiResponseHelper.ErrorResponse("Invalid request", "Invalid request payload.");
                }

                // Extract token from request headers
                var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var claims = _tokenService.DecodeToken(token);

                //if (claims == null || claims.Count == 0)
                //{
                //    return ApiResponseHelper.AuthErrorResponse("401", "Unauthorized access. Invalid token.");
                //}

                // Extract required claims from token
                if (!claims.TryGetValue("company", out string companyNo) ||
                    !claims.TryGetValue("location", out string locationNo) ||
                    !claims.TryGetValue("nameidentifier", out string empCode) ||
                    !claims.TryGetValue("User_Id", out string User_Id))
                {
                    return ApiResponseHelper.ErrorResponse("Data Missing", "Missing required data in token.");
                }

                if (!string.IsNullOrWhiteSpace(request.FromDate) && !IsValidDateFormat(request.FromDate))
                {
                    return ApiResponseHelper.ErrorResponse("Invalid FromDate format", "Invalid FromDate format. Expected format: yyyy-MM-dd, MM/dd/yyyy, or dd/MM/yyyy.");
                }
                if (!string.IsNullOrWhiteSpace(request.ToDate) && !IsValidDateFormat(request.ToDate))
                {
                    return ApiResponseHelper.ErrorResponse("Invalid ToDate format", "Invalid ToDate format. Expected format: yyyy-MM-dd, MM/dd/yyyy, or dd/MM/yyyy.");
                }

                //string Status = FilterHelper.ConvertStatusToValue(request.LeaveStatus);

                //string FromDateSession = FilterHelper.ConvertStatusToValue(request.FromDateSession);
                //string ToDateSession = FilterHelper.ConvertStatusToValue(request.ToDateSession);
                FilterHelper.LeaveActionMapping.TryGetValue(request.Action.ToUpper(), out string mappedValue);
                string actionValue = mappedValue; // mappedValue will be null if the key isn't found


                Hashtable ht = new Hashtable
                {
                    { "COMPANY_NO", companyNo },
                    { "LOCATION_NO", locationNo },
                    { "User_id", User_Id },
                    { "emp_code", empCode },
                    { "App_code", request.LeaveTransactionNo },
                    { "Leave_type", request.LeaveType },
                    { "From_date", request.FromDate },
                    { "To_date", request.ToDate },
                    { "From_session",request.FromDateSession },
                    { "To_session", request.ToDateSession },
                    { "Status", actionValue },
                    { "employee_reason", request.EmployeeReason },

                };

                // Fetch data from service
                var result = await _leaveService.LeaveAuthorizeDetailAsync(ht);

                return result;

                //if (result is ObjectResult objectResult)
                //{
                //    return objectResult;
                //}

                //return ApiResponseHelper.ErrorResponse("SERVER_ERROR", "Unexpected response format.");
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.ErrorResponse("500", ex.Message, "An unexpected error occurred.");
            }
        }

        private bool IsValidDateFormat(string dateStr)
        {
            if (string.IsNullOrWhiteSpace(dateStr))
            {
                return false;
            }

            string[] formats = { "yyyy-MM-dd", "MM/dd/yyyy", "dd/MM/yyyy" };
            return DateTime.TryParseExact(dateStr, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }
    }
}
