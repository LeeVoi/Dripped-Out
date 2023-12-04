using infrastructure.Repositories;
using service.Helpers;

namespace service.Services
{
    public class LoginService
    {
        private readonly LoginRepository _loginRepository;
        private readonly AuthenticationHelper _authentication;

        public LoginService(LoginRepository loginRepository, AuthenticationHelper authenticationHelper)
        {
            _loginRepository = loginRepository;
            _authentication = authenticationHelper;
        }
        
        
        
        
    }
}