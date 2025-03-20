using System.Collections;
using DEMO.Models.BusinessDL.Interfaces;
using DEMO.Models.DataDL.Classes;
using DEMO.Models.DTO.ApplyLeave;
using DEMO.Models.DTO.LeaveGrantReject;
using DEMO.SwaggerExamples;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace DEMO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetLeaveDetailsController : ControllerBase
    {

        private readonly TokenService _tokenService;
        private readonly ILeaveService _leaveService;

        public GetLeaveDetailsController(TokenService tokenService, ILeaveService leaveService)
        {
            _tokenService = tokenService;
            _leaveService = leaveService;
        }

        [HttpPost("GetLeaveDetails")]
        [SwaggerRequestExample(typeof(LeaveRequestDto), typeof(LeaveDetailExamples))]
        public async Task<IActionResult> GetLeaveDetails([FromBody] LeaveRequestDto request)
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
                    !claims.TryGetValue("location", out string locationNo) || !claims.TryGetValue("User_Id", out string User_Id) ||
                    !claims.TryGetValue("nameidentifier", out string empCode))
                {
                    return ApiResponseHelper.ErrorResponse("400", "Missing required data in token.");
                }

                if (request == null )
                {
                    return ApiResponseHelper.ErrorResponse("400", "Invalid request payload.");
                }

                string fromDate = string.IsNullOrWhiteSpace(request.FromDate) ? "" : request.FromDate;
                string toDate = string.IsNullOrWhiteSpace(request.ToDate) ? "" : request.ToDate;

                if (!(fromDate == "" || IsValidDateFormat(fromDate)) || !(toDate == "" || IsValidDateFormat(toDate)))
                {
                    return ApiResponseHelper.ErrorResponse("400", "Invalid date format. Expected format: yyyy-MM-dd or MM/dd/yyyy.");
                }

                // Prepare filters
                string filters = request.Filters != null && request.Filters.Any()
                    ? string.Join(",", request.Filters)
                    : null;

                Hashtable ht = new Hashtable
            {
                { "Asso_code", empCode },
                { "USER_ID", User_Id },
                { "Company_No", companyNo },
                { "Location_No", locationNo },
                { "STARTDATE", fromDate },
                { "ENDDATE", toDate },
                { "STATUS", string.IsNullOrEmpty(filters) ? (object)DBNull.Value : filters }
            };

                // Fetch data from service
                var result = await _leaveService.GetEmployeeLeaveDetailsAsync(ht);

                if (result == null)
                {
                    return ApiResponseHelper.ErrorResponse("404", "No leave data found.");
                }

                return result;
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.ErrorResponse("500", "An unexpected error occurred.", ex.Message);
            }
        }

        private bool IsValidDateFormat(string date)
        {
            return DateTime.TryParseExact(date, new[] { "yyyy-MM-dd", "MM/dd/yyyy" },
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out _);
        }
    }
}
