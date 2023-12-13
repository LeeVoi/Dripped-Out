using infrastructure.Entities;

namespace infrastructure.Repositories.Interface
{
    public interface ISizeMapper
    {
        void AddSizesToProduct(int productId, List<int> sizeIds);

        void RemoveSizesFromProduct(int productId, List<int> sizeIds);

        List<SizeType> GetAllSizesByProductId(int productId);

        List<SizeType> GetAllSizeTypes();
        
    }
}