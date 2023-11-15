using System.Linq;

namespace infrastructure.Entities
{
    public class Products
    {
        
        #region Properties
        
        public int ProductId { get; set; }
        
        public string ProductName { get; set; }
        
        public int TypeId { get; set; }
        
        public int Rating { get; set; }
        
        public decimal Price { get; set; } 
        
        #endregion
        
        

        
        public Products (string productName, int typeId, int rating, decimal price)
        {
            
            ProductName = productName;
            TypeId = typeId;
            Rating = rating;
            Price = price;
            
        }
        
        public bool IsHighlyRated()
        {
            // The product is highly rated if the rating is 4 or more
            return Rating >= 4;
        }
        
        public bool IsValidProductName()
        {
            // The product name is valid if it is not empty and contains only letters (Include Spacing)
            return !string.IsNullOrEmpty(ProductName) && ProductName.All(char.IsLetter);
        }
    }
    
}