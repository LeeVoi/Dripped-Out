using System.Text.Json;
using infrastructure.Entities;
using infrastructure.Entities.Helper;
using infrastructure.Repositories;
using infrastructure.Repositories.Factory;
using infrastructure.Repositories.Interface;
using service.Helpers;

namespace service.Services
{
    public class LoginService
    {
        private readonly ICrud<PasswordHash> _loginRepository;
        private readonly ILoginMapper _loginMapper;
        private readonly ICrud<Users> _userRepository;
        private readonly AuthenticationHelper _authentication;

        public LoginService(AuthenticationHelper authenticationHelper, CRUDFactory crudFactory, LoginRepository loginRepository)
        {
            _loginRepository = crudFactory.GetRepository<PasswordHash>(RepoType.LoginRepo);
            _loginMapper = loginRepository;
            _userRepository = crudFactory.GetRepository<Users>(RepoType.UserRepo);
            _authentication = authenticationHelper;
        }

        public bool Register(string email, string password)
        {
            var user = new Users
            {
                UserId = 0,
                Email = email,
                IsAdmin = false
            };

            byte[] passwordHash;
            byte[] passwordSalt;

            

            if (UserExists(email))
                return false;
            
            
            _userRepository.Create(user);
            _authentication.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var createdUser = _loginMapper.GetUserByEmail(email);
            
            if (createdUser == null)
                return false;
            
            var passwordObject = new PasswordHash
            {
                UserId = createdUser.UserId,
                Hash = passwordHash, 
                Salt = passwordSalt, 
                Algorithm = "HMACSHA512"
            };
            _loginRepository.Create(passwordObject); 
            return true;
            
        }

        public bool Login(string email, string password)
        {
            var user = _loginMapper.GetUserByEmail(email);
            
            if (user == null)
                return false;

            PasswordHash passwordHash = _loginRepository.Read(user.UserId);

            if (!_authentication.VerifyPasswordHash(password, passwordHash.Hash, passwordHash.Salt))
                return false;

            return true;
        }

        public void Update(string email, string password)
        {
            var user = _loginMapper.GetUserByEmail(email);
            byte[] passwordHash;
            byte[] passwordSalt;
            
            _authentication.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var passwordObject = new PasswordHash
            {
                UserId = user.UserId,
                Hash = passwordHash,
                Salt = passwordSalt,
                Algorithm = "HMACSHA512"
            };
            _loginRepository.Update(passwordObject);
        }

        public bool UserExists(string email)
        {
            if (_loginMapper.GetUserByEmail(email) == null)
                return false;

            return true;
        }

        public string GenerateToken(string email)
        {
            var user = _loginMapper.GetUserByEmail(email);
            return _authentication.GenerateToken(user);
        }
        
        
        
        
    }
}
