using System.Collections;
using System.Threading.Tasks;
using DEMO.Models.BusinessDL;
using DEMO.Models.DTO.OrgChartDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using DEMO.Models.DTO;
using DEMO.Models.DataDL.Classes;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace DEMO.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrgChartController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly OrgChartServices _OrgChart;
        private readonly ILogger<OrgChartController> _logger;
        private readonly TokenService _tokenService;

        public OrgChartController(IConfiguration configuration, OrgChartServices orgChart, ILogger<OrgChartController> logger, TokenService tokenService)
        {
            _configuration = configuration;
            _OrgChart = orgChart;
            _logger = logger;
            _tokenService = tokenService;
        }



        [Authorize]
        [HttpGet("OrgChartData")]
        public async Task<IActionResult> EmployeeOrgDetail(string ASSO_CODE)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var claims = _tokenService.DecodeToken(token);

                if (claims == null)
                {
                    return ApiResponseHelper.AuthErrorResponse("TOKEN_EXPIRED", "Your session has expired. Please log in again.");
                }

                //COMPANY_NO ??= claims.GetValueOrDefault("company");
                //LOCATION_NO ??= claims.GetValueOrDefault("location");

                //if (string.IsNullOrEmpty(COMPANY_NO) || string.IsNullOrEmpty(LOCATION_NO))
                //{
                //    return ApiResponseHelper.ErrorResponse("BAD_REQUEST", "Company and Location are required.");
                //}

                var parameters = new Hashtable
        {
            { "ASSO_CODE", ASSO_CODE }
            //{ "COMPANY_NO", COMPANY_NO } ,
            //{ "LOCATION_NO", LOCATION_NO }
        };

                // Call the service method that returns IActionResult
                IActionResult response = await _OrgChart.GetEmpOrganisationDetailAsync(parameters);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in EmployeeDetail: {ex.Message}");

                return ApiResponseHelper.ErrorResponse(
                    "INTERNAL_ERROR",
                    "An error occurred while processing the request.",
                    ex.Message
                );
            }
        }
     
    }
}







