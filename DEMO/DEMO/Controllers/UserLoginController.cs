using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using static DEMO.Models.DTO.UserLogin.UserLoginInfo;
using DEMO.Models.BusinessDL.Interfaces;
using DEMO.Models.DataDL.Classes;
using DEMO.Models.DTO.ApplyLeave;
using Swashbuckle.AspNetCore.Filters;
using DEMO.SwaggerExamples;


[Route("api/[controller]")]
[ApiController]
public class UserLoginController : ControllerBase
{
  
    private readonly TokenService _tokenService;
    private readonly IUserInterface _userService;

    public UserLoginController( TokenService tokenService, IUserInterface userService)
    {
        
        _tokenService = tokenService;
        _userService = userService;
    }



    /// <summary>
    ///  THIS IS A LOGIN API FOR THE ASSOCIATE IN WHICH WE GENERATE A TOKEN BASED ON LOGINNAME AND PASSWORD
    /// </summary>
    [HttpPost("login")]
    [SwaggerRequestExample(typeof(LoginRequest), typeof(LoginExamples))]
    public async Task<IActionResult> Loginn([FromBody] LoginRequest request)
    {
        var user = await _userService.ValidateUserAsync(request.LOGIN_NAME, request.PASSWORD);

        if (user != null)
        {
            var token = _tokenService.GenerateToken(user);

            return ApiResponseHelper.SuccessResponse( token);

        }

        return ApiResponseHelper.ErrorResponse("Invalid Data", "Invalid credentials", "The login name or password is incorrect.");
    }



    /// <summary>
    ///  THIS API IS USED TO DECODE THE TOKEN IN WHICH IT WILL EXTRACT NECESSARY INFORMATION FROM THE TOKEN. 
    /// </summary>
    [HttpGet("decodeToken")]
    public IActionResult DecodeToken([FromQuery] string token)
    {
        var claims = _tokenService.DecodeToken(token);

        if (claims == null)
        {
            return ApiResponseHelper.AuthErrorResponse("401", "Your session has expired. Please log in again.");
        }

        return ApiResponseHelper.SuccessResponse(new { claims });
    }

}





