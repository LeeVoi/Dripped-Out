using infrastructure.Entities;
using infrastructure.Entities.Helper;
using infrastructure.Repositories;
using infrastructure.Repositories.Factory;
using infrastructure.Repositories.Interface;

namespace service.Services;

public class ProductService
{
    private readonly ICrud<Products> _productRepository;
    private readonly IProductMapper _productMapperRepository;

    public ProductService(CRUDFactory crudFactory, ProductRepository productRepository)
    {
        _productRepository = crudFactory.GetRepository<Products>(RepoType.ProductRepo);
        _productMapperRepository = productRepository;
    }

    public Products getProductById(int productId)
    {
        return _productRepository.Read(productId);
    }

    public Products createProduct(Products products)
    {
        return _productRepository.Create(products);
    }

    public void updateProduct(Products products)
    {
        _productRepository.Update(products);
    }

    public void deleteProduct(int productId)
    {
        _productRepository.Delete(productId);
    }

    public IEnumerable<Products> getAllProducts()
    {
        return _productMapperRepository.getAllProducts();
    }

    public List<Products> getProductByType(int typeId)
    {
        return _productMapperRepository.getProductbyType(typeId);
    }

    public List<Products> getProductByGenderType(int typeId, string gender)
    {
        return _productMapperRepository.getProductbyGenderType(typeId, gender);
    }

    public List<Products> getProductByGender(string gender)
    {
        return _productMapperRepository.getProductbyGender(gender);
    }
}