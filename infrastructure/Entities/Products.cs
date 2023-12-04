using System.Linq;

namespace infrastructure.Entities
{
    public class Products
    {
        
        #region Properties
        
        public int ProductId { get; set; }
        
        public string ProductName { get; set; }
        
        public int TypeId { get; set; }
        
        public decimal Price { get; set; } 
        
        public string Gender { get; set; }
        
        public string Description { get; set; }
        
        #endregion
        

        
        public bool IsValidProductName()
        {
            // The product name is valid if it is not empty and contains only letters (Include Spacing)
            return !string.IsNullOrEmpty(ProductName) && ProductName.All(char.IsLetter);
        }
    }
    
}