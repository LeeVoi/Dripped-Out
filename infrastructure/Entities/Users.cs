using System.Text.RegularExpressions;

namespace infrastructure.Entities
{
    public class Users
    {
        
        #region Properties
        
        public int UserId { get; set; }
        
        public string Email { get; set; }
        
        public bool IsAdmin { get; set; }
        
        #endregion
        
        public bool IsUserAdmin()
        {
            return IsAdmin;
        }
        
        public bool IsEmailValid()
        {
            // Definition for a regex to insure a valid email address
            var emailRegex = new Regex(
                @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$");
            
            // Check if the email matches the regex definition
            return emailRegex.IsMatch(Email);
        }
    }
}