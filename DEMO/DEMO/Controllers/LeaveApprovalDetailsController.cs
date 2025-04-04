using System.Collections;
using DEMO.Models.BusinessDL.Interfaces;
using DEMO.Models.DataDL.Classes;
using DEMO.Models.DTO.ApplyLeave;
using DEMO.Models.DTO.LeaveApproval;
using DEMO.Models.Generic;
using DEMO.SwaggerExamples;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using static DEMO.Models.Generic.Enums;

namespace DEMO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveApprovalDetailsController : ControllerBase
    {

        private readonly TokenService _tokenService;
        private readonly ILeaveService _leaveService;

        public LeaveApprovalDetailsController(TokenService tokenService, ILeaveService leaveService)
        {
            _tokenService = tokenService;
            _leaveService = leaveService;
        }



        /// <summary>
        ///  THIS API IS USED TO APPROVE THE APPLIED LEAVE BY THE ASSOCIATE 
        /// </summary>
        [HttpPost("GetLeaveApprovalDetails")]
        [SwaggerRequestExample(typeof(LeaveRequestDto), typeof(LeaveApprovalDetailExamples))]
        public async Task<IActionResult> LeaveApprovalDetails([FromBody] LeaveRequestDto request)
        {
            try
            {
                // Extract token from request headers
                var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var claims = _tokenService.DecodeToken(token);

                if (claims == null || claims.Count == 0)
                {
                    return ApiResponseHelper.AuthErrorResponse("TOKEN_EXPIRED", "Your session has expired. Please log in again.");
                }

                // Extract required claims from token
                if (!claims.TryGetValue("company", out string companyNo) ||
                    !claims.TryGetValue("location", out string locationNo) || 
                    !claims.TryGetValue("User_Id", out string User_Id) ||
                    !claims.TryGetValue("nameidentifier", out string empCode))
                {
                    return ApiResponseHelper.ErrorResponse("Missing data", "Missing required data in token.");
                }

                if (request == null )
                {
                    return ApiResponseHelper.ErrorResponse("Invalid request", "Invalid request payload.");
                }

                string fromDate = request.DateRange?.FromDate ?? "";
                string toDate = request.DateRange?.ToDate ?? "";
                if (!(fromDate == "" || IsValidDateFormat(fromDate)) || !(toDate == "" || IsValidDateFormat(toDate)))
                {
                    return ApiResponseHelper.ErrorResponse("Invalid date format", "Invalid date format. Expected format: yyyy-MM-dd or MM/dd/yyyy.");
                }

                //string filters = FilterHelper.ConvertStatusToNumber(request.Status).ToString();

                string filters = "";
                string directValue = "";

                if (request.filters != null)
                {
                    foreach (var status in request.filters)
                    {
                        
                        if (status.ToString() == "DIRECT")
                        {
                            directValue = "1"; 
                        }
                        else
                        {
                            
                            string statusValue = FilterHelper.ConvertStatusToValue(status).ToString();

                            if (filters == "")
                            {
                                filters = statusValue;
                            }
                            else
                            {
                                filters += "," + statusValue;
                            }
                        }
                    }
                }

                //string statusValue1 = FilterHelper.ConvertNumberToEnum(statusValue).ToString();

                Hashtable ht = new Hashtable
            {
                { "Asso_code", empCode },
                { "USER_ID", User_Id },
                { "Company_No", companyNo },
                { "Location_No", locationNo },
                { "STARTDATE", fromDate },
                { "ENDDATE", toDate },
                { "DIRECT", directValue },
                { "STATUS", string.IsNullOrEmpty(filters) ? (object)DBNull.Value : filters }

            };

                // Fetch data from service
                var result = await _leaveService.GetEmployeeLeaveApprovalDetailsAsync(ht);

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
