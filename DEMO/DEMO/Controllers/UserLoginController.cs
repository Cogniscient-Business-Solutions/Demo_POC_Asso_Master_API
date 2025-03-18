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


   
    [HttpPost("login")]
    [SwaggerRequestExample(typeof(LoginRequest), typeof(LoginExamples))]
    public async Task<IActionResult> Loginn([FromBody] LoginRequest request)
    {
        var user = await _userService.ValidateUserAsync(request.LOGIN_NAME, request.PASSWORD);

        if (user != null)
        {
            var token = _tokenService.GenerateToken(user.UserId, user.Company, user.Location, user.User_Id, user.Role);

            return ApiResponseHelper.SuccessResponse(new { status = "success", token });
        }

        return ApiResponseHelper.AuthErrorResponse("401", "Invalid credentials", "The login name or password is incorrect.");
    }

    [HttpGet("decodeToken")]
    public IActionResult DecodeToken([FromQuery] string token)
    {
        var claims = _tokenService.DecodeToken(token);

        if (claims == null)
        {
            return BadRequest(new { status = "fail", message = "Invalid token" });
        }

        return Ok(new
        {
            status = "success",
            claims
        });
    }

}





