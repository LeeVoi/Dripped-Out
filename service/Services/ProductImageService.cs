using infrastructure.DatabaseManager;
using infrastructure.Entities;
using infrastructure.Entities.Helper;
using infrastructure.Repositories;
using infrastructure.Repositories.Factory;
using infrastructure.Repositories.Interface;

namespace service.Services;

public class ProductImageService
{
    private ICrud<ProductImage> _repository;
    private IProductImageMapper _imageMapper;
    
    public ProductImageService(CRUDFactory crudFactory, IProductImageMapper imageMapper)
    {
        _repository = crudFactory.GetRepository<ProductImage>(RepoType.ProductImageRepo);
        _imageMapper = imageMapper;
    }
    public ProductImage Create(ProductImage productImage)
    {
       return _repository.Create(productImage);
    }

    public ProductImage GetProductImage(int productId, int colorId)
    {
        return _imageMapper.getProductImage(productId, colorId);
    }

    public IEnumerable<ProductImage> GetAllProductsImages(int productId)
    {
        return _imageMapper.getAllProductsImages(productId);
    }
}