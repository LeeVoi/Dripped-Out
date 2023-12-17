using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using infrastructure.Entities;
using infrastructure.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace service.Helpers
{
    public class AuthenticationHelper
    {
        private byte[] secretBytes;

        public AuthenticationHelper(Byte[] secret)
        {
            secretBytes = secret;
        }
        
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i])
                        return false;
                }
            }
            return true;
        }

        public string GenerateToken(Users user)
        {

            var userId = user.UserId.ToString();
            var email = user.Email;
            var isadmin = user.IsAdmin.ToString();
            var claims = new List<Claim>
            {
                new Claim("id", userId),
                new Claim("email", email),
                new Claim("isadmin", isadmin)
            };

            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    new SymmetricSecurityKey(secretBytes),
                    SecurityAlgorithms.HmacSha256)),
                new JwtPayload(null, null, claims.ToArray(), DateTime.Now, DateTime.Now.AddHours(8)));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}