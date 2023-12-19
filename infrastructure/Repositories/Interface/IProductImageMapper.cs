using infrastructure.Entities;

namespace infrastructure.Repositories.Interface;

public interface IProductImageMapper
{
    ProductImage getProductImage(int productId, int colorId);

    IEnumerable<ProductImage> getAllProductsImages(int productId);
}