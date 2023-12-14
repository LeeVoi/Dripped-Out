using Microsoft.AspNetCore.Mvc;
using service.Services;

namespace api.Controllers;

[ApiController]
public class LoginController : ControllerBase
{
    private readonly LoginService _loginService;

    public LoginController(LoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost("/registeruser")]
    public bool Register([FromBody] LoginDto loginDto)
    {
        return _loginService.Register(loginDto.Email, loginDto.Password);
    }

    [HttpPost("/loginuser")]
    public IActionResult Login([FromBody] LoginDto loginDto)
    {
        var email = loginDto.Email;
        var password = loginDto.Password;
        if (!_loginService.UserExists(email))
            return Unauthorized();

        if (_loginService.Login(email, password))
        {
            var token = _loginService.GenerateToken(email);
            return Ok(new { Token = token });
        }

        return Unauthorized();

    }

    [HttpPost("/updateuser")]
    public void Update([FromBody] LoginDto loginDto)
    {
        _loginService.Update(loginDto.Email, loginDto.Password);
    }

    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}