using DEMO.Models.DataDL.Classes;
using DEMO.Models.Generic;
using System.Collections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DEMO.Models.BusinessDL.Interfaces;
using static DEMO.Models.DTO.LeaveGrantReject.LeaveGrantReject;
using DEMO.SwaggerExamples;
using Swashbuckle.AspNetCore.Filters;
using Newtonsoft.Json;


namespace DEMO.Controllers
{
    [Route("API/[controller]")]
    [ApiController]
    public class LeaveGrantRejectController : ControllerBase
    {

        private readonly TokenService _tokenService;
        private readonly ILeaveService _leaveService;

        public LeaveGrantRejectController(TokenService tokenService, ILeaveService leaveService)
        {
            _tokenService = tokenService;
            _leaveService = leaveService;
        }



        /// <summary>
        ///  THIS API IS USED FOR LEAVE GRANT , REJECT AND CANCEL .
        /// </summary>
        [HttpPost("GetLeaveGrantRejectDetails")]
        [SwaggerRequestExample(typeof(LeaveGrantRejectRequest), typeof(LeaveGrantRejectExamples))]
        public async Task<IActionResult> LeaveGrantReject([FromBody] LeaveGrantRejectRequest request)
        {
            try
            {
                // Extract token from request headers
                var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var claims = _tokenService.DecodeToken(token);

                if (claims == null || claims.Count == 0)
                {
                    return ApiResponseHelper.AuthErrorResponse("Invalid token", "Unauthorized access. Invalid token.");
                }

                // Extract required claims from token
                if (!claims.TryGetValue("company", out string companyNo)  ||
                    !claims.TryGetValue("location", out string locationNo) || !claims.TryGetValue("User_Id", out string User_Id) ||
                    !claims.TryGetValue("nameidentifier", out string empCode))
                {
                    return ApiResponseHelper.ErrorResponse("Missing Data", "Missing required data in token.");
                }

                if (request == null)
                {
                    return ApiResponseHelper.ErrorResponse("Invalid request", "Invalid request payload.");
                }

                foreach (var leave in request.Leaves)
                {
                    if (FilterHelper.LeaveStatusMapping.TryGetValue(leave.leaveStatus, out string statusValue))
                    {
                        leave.leaveStatus = statusValue; 
                    }
                }


                string leaveDataJson = JsonConvert.SerializeObject(request.Leaves);
               

                Hashtable ht = new Hashtable
                {
                { "Asso_code", empCode },
                { "USER_ID", User_Id },
                { "Company_No", companyNo },
                { "Location_No", locationNo },
                {"API_INPUT_JASON_DATA",leaveDataJson },
                };

                // Fetch data from service
                var result = await _leaveService.LeaveGrantRejectDetailAsync(ht);

                if (result == null)
                {
                    return ApiResponseHelper.ErrorResponse("No Data", "No leave data found.");
                }

                return result;
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.ErrorResponse("500", "An unexpected error occurred.", ex.Message);
            }
        }
    }
}
