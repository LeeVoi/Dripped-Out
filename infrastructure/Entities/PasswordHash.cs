namespace infrastructure.Entities
{
    public class PasswordHash
    {
        #region Properties
        
        public int UserId { get; set; }
        
        public string Hash { get; set; }
        
        public string Salt { get; set; }
        
        public string Algorithm { get; set; }
        
        #endregion
    }
}