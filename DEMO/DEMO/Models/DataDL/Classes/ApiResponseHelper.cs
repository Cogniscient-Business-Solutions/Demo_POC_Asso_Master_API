using Microsoft.AspNetCore.Mvc;

namespace DEMO.Models.DataDL.Classes
{
    public static class ApiResponseHelper
    {
        public static IActionResult SuccessResponse<T>(T data)
        {
            var response = new
            {
                status = "success",
                data
            };
            return new OkObjectResult(response);
        }

        public static IActionResult ErrorResponse(string errorCode, string errorMessage, string details = "")
        {
            var response = new
            {
                status = "fail",
                error = new
                {
                    code = errorCode,
                    details,
                    message = errorMessage,
                    
                }
            };
            return new ObjectResult(response) { StatusCode = 404 };
        }


        public static IActionResult AuthErrorResponse(string errorCode, string errorMessage, string details = "")
        {
            var response = new
            {
                status = "FAIL",
                error = new
                {
                    code = errorCode,
                    message = errorMessage,
                    details
                }
            };

            return new ObjectResult(response) { StatusCode = 401 }; 
        }


    }
}
