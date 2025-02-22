using System.Collections;
using DEMO.Models.BusinessDL;
using DEMO.Models.DTO.EmpDetail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DEMO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetEmployeeByIdController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly GetEmployeeByIdService _GetEmployeeByIdService;
        private readonly Hashtable objht = new Hashtable();
        private readonly ILogger<GetEmployeeByIdController> _logger;

        public GetEmployeeByIdController(IConfiguration cnfg, GetEmployeeByIdService GetEmployeeByIdService, ILogger<GetEmployeeByIdController> logger) // Inject logger
        {
            _configuration = cnfg;
            _GetEmployeeByIdService = GetEmployeeByIdService;
            _logger = logger; 
        }


        //abhaybackup
        //[HttpGet("EmpDetail")]
        //public async Task<IActionResult> EmployeeDetail(string ASSO_CODE, string COMPANY_NO, string LOCATION_NO)
        //{
        //    try
        //    {

        //        objht.Clear();

        //        objht.Add("ASSO_CODE", ASSO_CODE ?? "");
        //        objht.Add("COMPANY_NO", COMPANY_NO ?? "");
        //        objht.Add("LOCATION_NO", LOCATION_NO ?? "");


        //        _GetEmployeeByIdService.ht = objht;


        //        var response = await _GetEmployeeByIdService.GetEmpDetailAsync();


        //        var finalResponse = new
        //        {
        //            EmpDetailResult = new
        //            {
        //                EDetails = response.EDetails,
        //                EmpMessage = new
        //                {
        //                    ErrorMsg = response.EmpMessage.ErrorMsg,
        //                    Success = response.EmpMessage.Success
        //                }
        //            }
        //        };


        //        if (response.EmpMessage.Success)
        //        {
        //            return Ok(finalResponse);
        //        }
        //        else
        //        {
        //            return NotFound(finalResponse);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        var errorResponse = new
        //        {
        //            EmpDetailResult = new
        //            {
        //                EDetails = new List<EmpDetail>(),
        //                EmpMessage = new EmpMsg
        //                {
        //                    ErrorMsg = ex.Message,
        //                    Success = false
        //                }
        //            }
        //        };


        //        return BadRequest(errorResponse);
        //    }
        //}


        [HttpGet("EmpDetail")]
        public async Task<IActionResult> EmployeeDetail(string ASSO_CODE, string COMPANY_NO, string LOCATION_NO)
        {
            try
            {
                objht.Clear();
                objht.Add("ASSO_CODE", ASSO_CODE ?? "");
                objht.Add("COMPANY_NO", COMPANY_NO ?? "");
                objht.Add("LOCATION_NO", LOCATION_NO ?? "");

                _GetEmployeeByIdService.ht = objht;

                var response = await _GetEmployeeByIdService.GetEmpDetailAsync();

                if (response.EmpMessage.Success)
                {
                    var successResponse = new
                    {
                        status = "SUCCESS",
                        data = new
                        {
                            EDetails = response.EDetails
                        }
                    };

                    return Ok(successResponse);
                }
                else
                {
                    var errorResponse = new
                    {
                        status = "FAIL",
                        error = new
                        {
                            code = "USER_NOT_FOUND",
                            message = response.EmpMessage.ErrorMsg,
                            details = "Please check the employee ID and try again."
                        }
                    };

                    return NotFound(errorResponse);
                }
            }
            catch (Exception ex)
            {
                var errorResponse = new
                {
                    status = "FAIL",
                    error = new
                    {
                        code = "SERVER_ERROR",
                        message = "An unexpected error occurred.",
                        details = ex.Message
                    }
                };

                return BadRequest(errorResponse);
            }
        }

    }
}
