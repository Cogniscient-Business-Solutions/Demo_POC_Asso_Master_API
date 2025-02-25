using System.Collections;
using System.Threading.Tasks;
using DEMO.Models.BusinessDL;
using DEMO.Models.DTO.OrgChartDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using DEMO.Models.DTO;
using DEMO.Models.DataDL.Classes;

namespace DEMO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrgChartController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly OrgChartServices _OrgChart;
        private readonly ILogger<OrgChartController> _logger;

        public OrgChartController(IConfiguration configuration, OrgChartServices orgChart, ILogger<OrgChartController> logger)
        {
            _configuration = configuration;
            _OrgChart = orgChart;
            _logger = logger;
        }

        [HttpGet("OrgChartData")]
        public async Task<IActionResult> EmployeeDetail(string ASSO_CODE, string COMPANY_NO, string LOCATION_NO)
        {
            try
            {
                var parameters = new Hashtable
        {
            { "ASSO_CODE", ASSO_CODE ?? "" },
            { "COMPANY_NO", COMPANY_NO ?? "" },
            { "LOCATION_NO", LOCATION_NO ?? "" }
        };

                var response = await _OrgChart.GetEmpDetailAsync(parameters);

                return response.Status == "SUCCESS"
                    ? ApiResponseHelper.SuccessResponse(response)
                    : ApiResponseHelper.ErrorResponse("NOT_FOUND", "Employee details not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in EmployeeDetail: {ex.Message}");
                return ApiResponseHelper.ErrorResponse("INTERNAL_ERROR", "An error occurred while processing the request.", ex.Message);
            }
        }

    }
}
