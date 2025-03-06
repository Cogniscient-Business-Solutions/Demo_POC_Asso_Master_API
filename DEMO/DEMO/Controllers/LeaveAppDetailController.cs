using System.Collections;
using System.Globalization;
using System.Reflection.Emit;
using DEMO.Models.BusinessDL;
using DEMO.Models.DataDL.Classes;
using DEMO.Models.DTO.LeaveAppDetail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DEMO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveAppDetailController : ControllerBase
    {

        private readonly Hashtable objht = new Hashtable();
        private readonly TokenService _tokenService;
        private readonly LeaveAppDetailService _leaveAppDetailService;

        public LeaveAppDetailController(LeaveAppDetailService leaveAppDetailService, TokenService tokenService)
        {
            _tokenService = tokenService;
            _leaveAppDetailService = leaveAppDetailService;
        }

       
        [HttpPost("GetLeaveAppDetails")]
        public async Task<IActionResult> GetLeaveAppDetails([FromBody] GetLeaveRequestDto request)
        {
            try
            {
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
                    !claims.TryGetValue("nameidentifier", out string empCode)) 
                {
                    return ApiResponseHelper.ErrorResponse("400", "Missing required data in token.");
                }

                
                if (request == null)
                {
                    return ApiResponseHelper.ErrorResponse("400", "Invalid request payload.");
                }

                string fromDate = string.IsNullOrWhiteSpace(request.FromDate) ? null : request.FromDate;
                string toDate = string.IsNullOrWhiteSpace(request.ToDate) ? null : request.ToDate;

                if ((fromDate != null && !IsValidDateFormat(fromDate)) ||
                    (toDate != null && !IsValidDateFormat(toDate)))
                {
                    return ApiResponseHelper.ErrorResponse("400", "Invalid date format. Expected format: yyyy-MM-dd or MM/dd/yyyy.");
                }


                Hashtable ht = new Hashtable
        {
            { "Emp_Code", empCode },
            { "Company_No", companyNo },
            { "Location_No", locationNo },
            { "StartDate", request.FromDate },
            { "EnddDate", request.ToDate }
        };

                // Fetch data from service
                var result = await _leaveAppDetailService.GetLeaveAppDetailAsync(ht);
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
