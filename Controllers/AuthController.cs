using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] User user)
    {
        var token = _userService.Authenticate(user.Username, user.Password);
        if (token == null) return Unauthorized();

        return Ok(new { token });
    }

    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok("API is working!");
    }
}
