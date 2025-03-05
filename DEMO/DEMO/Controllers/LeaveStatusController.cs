using System.Collections;
using DEMO.Models.BusinessDL;
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
        private readonly LeaveStatusService _leaveStatusService;

        public LeaveStatusController(LeaveStatusService leaveStatusService, TokenService tokenService)
        {
            _tokenService = tokenService;
            _leaveStatusService = leaveStatusService;
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
                    return ApiResponseHelper.AuthErrorResponse("401", "Unauthorized access. Invalid token.");
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
                var response = await _leaveStatusService.GetLeaveStatusDetailAsync(ht);
                if (response is ObjectResult objectResult)
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

        //public async Task<IActionResult> GetLeaveStatusDetail()
        //{
        //    try
        //    {
        //        // Extract token from request headers
        //        var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        //        var claims = _tokenService.DecodeToken(token); // This returns Dictionary<string, string>

        //        if (claims == null || claims.Count == 0)
        //        {
        //            return Unauthorized(new { status = "fail", message = "Invalid token." });
        //        }


        //        if (!claims.TryGetValue("company", out string companyNo) ||
        //            !claims.TryGetValue("location", out string locationNo) ||
        //            !claims.TryGetValue("nameidentifier", out string assoCode))
        //        {
        //            return BadRequest(new { status = "fail", message = "Missing required data in token." });
        //        }

        //        // Create Hashtable with extracted values
        //        Hashtable ht = new Hashtable
        //    {
        //        { "company_no", companyNo },
        //        { "location_no", locationNo },
        //        { "asso_code", assoCode }
        //    };

        //        // Call the service method
        //        var response = await _leaveStatusService.GetLeaveStatusDetailAsync(ht);
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { status = "error", message = "An unexpected error occurred.", error = ex.Message });
        //    }
        //}
    }
}
