namespace infrastructure.Entities
{
    public class UserReviews
    {
        public int UserId { get; set; }
        
        public int ProductId { get; set; }
        
        public int Rating { get; set; }
        
        public string ReviewText { get; set; }
        
    }
}