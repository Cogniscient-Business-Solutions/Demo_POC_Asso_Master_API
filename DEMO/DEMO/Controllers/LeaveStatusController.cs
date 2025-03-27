using System.Collections;
using DEMO.Models.BusinessDL;
using DEMO.Models.BusinessDL.Interfaces;
using DEMO.Models.DataDL.Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DEMO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveStatusController : ControllerBase
    {

        private readonly Hashtable objht = new Hashtable();
        private readonly TokenService _tokenService;
        
        private readonly ILeaveService _leaveService;

        public LeaveStatusController( TokenService tokenService, ILeaveService leaveService)
        {
            _tokenService = tokenService;
            
            _leaveService = leaveService;
        }

      
        [HttpGet("GetLeaveStatusDetail")]
        public async Task<IActionResult> GetLeaveStatusDetail()
        {
            try
            {
                // Extract token from request headers
                var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var claims = _tokenService.DecodeToken(token); // This returns Dictionary<string, string>

                if (claims == null || claims.Count == 0)
                {
                    return ApiResponseHelper.AuthErrorResponse("TOKEN_EXPIRED", "Your session has expired. Please log in again.");
                }

                // Extract required claims from token
                if (!claims.TryGetValue("company", out string companyNo) ||
                    !claims.TryGetValue("location", out string locationNo) ||
                    !claims.TryGetValue("nameidentifier", out string assoCode))
                {
                    return ApiResponseHelper.ErrorResponse("400", "Missing required data in token.");
                }

                // Create Hashtable with extracted values
                Hashtable ht = new Hashtable
        {
            { "company_no", companyNo },
            { "location_no", locationNo },
            { "asso_code", assoCode }
        };

                // Call the service method
                var response = await _leaveService.GetLeaveStatusDetailAsync(ht);

                return response;


                //if (response is ObjectResult objectResult)
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

       
    }
}
