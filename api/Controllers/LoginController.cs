using service.Services;

namespace api.Controllers;

public class LoginController
{
    private readonly LoginService _loginService;

    public LoginController(LoginService loginService)
    {
        _loginService = loginService;
    }
}