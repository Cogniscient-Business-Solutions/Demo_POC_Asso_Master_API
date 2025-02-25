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

    [HttpGet]
    public IActionResult Login([FromQuery] string company, [FromQuery] string location, [FromQuery] string userId, [FromQuery] string password)
    {
        // Hardcoded authentication logic
        if (company == "ABC" && location == "NewYork" && userId == "admin" && password == "password123")
        {
            var token = _tokenService.GenerateToken(userId, company, location);
            return Ok(new
            {
                status = "success",
                token
            });
        }
        else
        {
            return Unauthorized(new
            {
                status = "fail",
                message = "Invalid credentials"
            });
        }
    }
}
