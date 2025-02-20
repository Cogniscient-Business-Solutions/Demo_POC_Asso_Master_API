using System.Collections;
using DEMO.Models.BusinessDL;
using DEMO.Models.DTO.OrgChartDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DEMO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrgChartController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly OrgChartServices _OrgChart;
        private readonly Hashtable objht = new Hashtable();
        private readonly ILogger<OrgChartController> _logger;

        public OrgChartController(IConfiguration cnfg, OrgChartServices OrgChart, ILogger<OrgChartController> logger) // Inject logger
        {
            _configuration = cnfg;
            _OrgChart = OrgChart;
            _logger = logger;
        }

        [HttpGet("OrgChartData")]
        public async Task<IActionResult> EmployeeDetail(string ASSO_CODE, string COMPANY_NO, string LOCATION_NO)
        {
            try
            {

                objht.Clear();

                objht.Add("ASSO_CODE", ASSO_CODE ?? "");
                objht.Add("COMPANY_NO", COMPANY_NO ?? "");
                objht.Add("LOCATION_NO", LOCATION_NO ?? "");


                _OrgChart.ht = objht;


                var response = await _OrgChart.GetEmpDetailAsync();


                var finalResponse = new
                {
                    EmpDetailResult = new
                    {
                        Empdetails = response.Empdetails,
                        EmpMessage = new
                        {
                            ErrorMsg = response.EmpMessage.ErrorMsg,
                            Success = response.EmpMessage.Success
                        }
                    }
                };


                if (response.EmpMessage.Success)
                {
                    return Ok(finalResponse);
                }
                else
                {
                    return NotFound(finalResponse);
                }
            }
            catch (Exception ex)
            {

                var errorResponse = new
                {
                    EmpDetailResult = new
                    {
                        EDetails = new List<OrgChartData>(),
                        EmpMessage = new LogMsg
                        {
                            ErrorMsg = ex.Message,
                            Success = false
                        }
                    }
                };


                return BadRequest(errorResponse);
            }
        }

    }
}

