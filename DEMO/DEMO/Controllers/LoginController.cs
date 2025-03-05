using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly TokenService _tokenService;

    public LoginController(TokenService tokenService)
    {
        _tokenService = tokenService;
    }

    //[HttpGet]
    //public IActionResult Login([FromQuery] string company, [FromQuery] string location, [FromQuery] string userId, [FromQuery] string password)
    //{
    //    // Hardcoded authentication logic
    //    if (company == "ABC" && location == "NewYork" && userId == "admin" && password == "password123")
    //    {
    //        var token = _tokenService.GenerateToken(userId, company, location);
    //        return Ok(new
    //        {
    //            status = "success",
    //            token
    //        });
    //    }
    //    else
    //    {
    //        return Unauthorized(new
    //        {
    //            status = "fail",
    //            message = "Invalid credentials"
    //        });
    //    }
    //}

    [HttpGet]
    public IActionResult Login([FromQuery] string company, [FromQuery] string location, [FromQuery] string Asso_code, [FromQuery] string password)
    {
        if (company == "cogni" && location == "noida")
        {
            var token = _tokenService.GenerateToken(Asso_code, company, location);

            return Ok(new
            {
                status = "success",
                token
            });
        }

        
        return Unauthorized(new
        {
            status = "fail",
            message = "Invalid credentials"
        });
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
