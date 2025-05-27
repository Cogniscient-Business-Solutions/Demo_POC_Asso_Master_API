using System.Collections;
using System.Globalization;
using System.Reflection.Emit;
using DEMO.Models.BusinessDL;
using DEMO.Models.BusinessDL.Interfaces;
using DEMO.Models.DataDL.Classes;
using DEMO.Models.DTO.EmpDetail;
using DEMO.Models.DTO.LeaveAppDetail;
using DEMO.Models.Generic;
using DEMO.SwaggerExamples;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using DEMO.Models.Generic;
using static DEMO.Models.Generic.Enums;
namespace DEMO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveAppDetailController : ControllerBase
    {

        private readonly Hashtable objht = new Hashtable();
        private readonly TokenService _tokenService;
       
        private readonly ILeaveService _leaveService;
        public LeaveAppDetailController(TokenService tokenService, ILeaveService leaveService)
        {
            _tokenService = tokenService;
            
            _leaveService = leaveService;
        }




        /// <summary>
        ///  THIS API IS USED TO SEE THE LEAVE DETAIL
        /// </summary>
        
        [HttpPost("GetLeaveAppDetails")]
        [SwaggerRequestExample(typeof(GetLeaveRequestDto), typeof(LeaveAppDetailExamples))]
        public async Task<IActionResult> GetLeaveAppDetails([FromBody] GetLeaveRequestDto request)
        {
            try
            {
                // Extract token from request headers
                var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var claims = _tokenService.DecodeToken(token); 

                if (claims == null || claims.Count == 0)
                {
                    return ApiResponseHelper.AuthErrorResponse("401", "Your session has expired. Please log in again.");
                }

                // Extract required claims from token
                if (!claims.TryGetValue("company", out string companyNo) ||
                    !claims.TryGetValue("location", out string locationNo) ||
                    !claims.TryGetValue("nameidentifier", out string empCode)) 
                {
                    return ApiResponseHelper.ErrorResponse("Invalid Data", "Missing required data in token.");
                }

                
                if (request == null)
                {
                    return ApiResponseHelper.ErrorResponse("Invalid request", "Invalid request payload.");
                }

                string fromDate = request.DateRange?.FromDate ?? "";
                string toDate = request.DateRange?.ToDate ?? "";
                if ( !(fromDate=="" || IsValidDateFormat(fromDate)) ||  !(toDate == "" || IsValidDateFormat(toDate)))
                {
                    return ApiResponseHelper.ErrorResponse("Invalid date format", "Invalid date format. Expected format: yyyy-MM-dd or MM/dd/yyyy.");
                }



                string statusString = request.LeaveStatus != null && request.LeaveStatus.Any()
                ? string.Join(",", request.LeaveStatus.Select(s => FilterHelper.ConvertStatusToValue(s)))
                : "";


                Hashtable ht = new Hashtable
                {
                    { "Emp_Code", empCode },
                    { "Company_No", companyNo },
                    { "Location_No", locationNo },
                    { "StartDate", fromDate },
                    { "EnddDate", toDate },
                    { "Status", string.IsNullOrEmpty(statusString) ? (object)DBNull.Value : statusString }
                };

                // Fetch data from service
                var result = await _leaveService.GetLeaveAppDetailAsync(ht);

                return result;

                //if (result is ObjectResult objectResult)
                //{
                //    return objectResult;
                //}

                //return ApiResponseHelper.ErrorResponse("SERVER_ERROR", "Unexpected response format.");
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
