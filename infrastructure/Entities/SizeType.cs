namespace infrastructure.Entities
{
    
    public class SizeType
    {

        #region Properties

        public int SizeId { get; set; }
        
        public string Size { get; set; }
        
        #endregion

        public SizeType(string size)
        {
            Size = size;
        }
        
    }
}