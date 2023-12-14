using infrastructure.Entities;

namespace infrastructure.Repositories.Interface
{
    public interface IColorMapper
    {
        void AddColorToProduct(int productId, List<int> colorIds);

        void RemoveColorFromProduct(int productId, List<int> colorIds);

        List<ColorType> GetColorsByProductId(int productId);

        List<ColorType> GetAllColorTypes();
    }
}