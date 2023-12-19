using infrastructure.DatabaseManager;
using infrastructure.Entities;
using infrastructure.Repositories;

namespace service.Services;

public class ProductImageService
{
    private ProductImageRepository _repository;
    
    public ProductImageService(ProductImageRepository repository)
    {
        _repository = repository;
    }
    public ProductImage Create(ProductImage productImage)
    {
       return _repository.Create(productImage);
    }

    public ProductImage GetProductImage(int productId, int colorId)
    {
        return _repository.getProductImage(productId, colorId);
    }

    public IEnumerable<ProductImage> GetAllProductsImages(int productId)
    {
        return _repository.getAllProductsImages(productId);
    }
}