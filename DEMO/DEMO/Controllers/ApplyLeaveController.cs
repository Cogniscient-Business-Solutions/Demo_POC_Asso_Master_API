using DEMO.Models.BusinessDL;
using System.Collections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DEMO.Models.DataDL.Classes;
using DEMO.Models.DTO.LeaveAppDetail;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using DEMO.Models.DTO.ApplyLeave;
using System.ComponentModel.Design;

namespace DEMO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplyLeaveController : ControllerBase
    {
        private readonly Hashtable objht = new Hashtable();
        private readonly TokenService _tokenService;
        private readonly ApplyLeaveService _applyLeaveDetailService;

        public ApplyLeaveController(ApplyLeaveService applyLeaveDetailService, TokenService tokenService)
        {
            _tokenService = tokenService;
            _applyLeaveDetailService = applyLeaveDetailService;
        }

        [Authorize]
        [HttpPost("ApplyLeaveDetail")]

        public async Task<IActionResult> ApplyLeaveDetail([FromBody] ApplyLeaveRequestDto request)
        {
            try
            {
                if (request == null)
                {
                    return ApiResponseHelper.ErrorResponse("400", "Invalid request payload.");
                }

                // Extract token from request headers
                var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var claims = _tokenService.DecodeToken(token);

                if (claims == null || claims.Count == 0)
                {
                    return ApiResponseHelper.AuthErrorResponse("401", "Unauthorized access. Invalid token.");
                }

                // Extract required claims from token
                if (!claims.TryGetValue("company", out string companyNo) ||
                    !claims.TryGetValue("location", out string locationNo) ||
                    !claims.TryGetValue("nameidentifier", out string empCode) ||
                    !claims.TryGetValue("User_Id", out string User_Id))
                {
                    return ApiResponseHelper.ErrorResponse("400", "Missing required data in token.");
                }

                if (!string.IsNullOrWhiteSpace(request.FromDate) && !IsValidDateFormat(request.FromDate))
                {
                    return ApiResponseHelper.ErrorResponse("400", "Invalid FromDate format. Expected format: yyyy-MM-dd, MM/dd/yyyy, or dd/MM/yyyy.");
                }
                if (!string.IsNullOrWhiteSpace(request.ToDate) && !IsValidDateFormat(request.ToDate))
                {
                    return ApiResponseHelper.ErrorResponse("400", "Invalid ToDate format. Expected format: yyyy-MM-dd, MM/dd/yyyy, or dd/MM/yyyy.");
                }

                List<int> statusList = StatusHelper.ConvertStatusList(request.LeaveStatus);

                // Ensure we only pass valid statuses to the procedure
                if (statusList.Count == 0)
                {
                    throw new ArgumentException("Invalid leave status values provided.");
                }

                Hashtable ht = new Hashtable
                {
                    { "COMPANY_NO", companyNo },
                    { "LOCATION_NO", locationNo },
                    { "TRANSACTION_MODE", "0" },
                    { "User_id", User_Id },
                    { "emp_code", empCode },
                    { "App_code", "" },
                    { "Leave_type", request.LeaveType },
                    { "Notified_date", DateTime.Now.ToString() },                                       
                    { "From_date", request.FromDate },
                    { "To_date", request.ToDate },
                    { "From_session", request.FromDateSession },
                    { "To_session", request.ToDateSession },
                    { "Status", statusList.Count == 0 ? (object)DBNull.Value : string.Join(",", statusList) },
                    { "employee_reason", request.EmployeeReason },
                    { "employer_reason", "" },
                  
              
                };

                                // Fetch data from service
                var result = await _applyLeaveDetailService.ApplyLeaveDetailAsync(ht);
                if (result is ObjectResult objectResult)
                {
                    return objectResult;
                }

                return ApiResponseHelper.ErrorResponse("SERVER_ERROR", "Unexpected response format.");
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.ErrorResponse("500", "An unexpected error occurred.", ex.Message);
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
