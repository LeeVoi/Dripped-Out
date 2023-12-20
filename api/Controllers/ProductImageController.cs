using api.Authorizers;
using infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using service.Services;

namespace api.Controllers;

public class ProductImageController : ControllerBase
{
    private ProductImageService _service;

    public ProductImageController(ProductImageService service)
    {
        _service = service;
    }

    [AuthorizeAdmin]
    [HttpPost("/api/product-image-create")]
    public ProductImage Create([FromBody]ProductImage productImage)
    {
        return _service.Create(productImage);
    }

    [HttpGet("/api/product-image-get")]
    public ProductImage GetProductImage(int productId, int colorId)
    {
        return _service.GetProductImage(productId, colorId);
    }

    [HttpGet("api/product-image-get-all")]
    public IEnumerable<ProductImage> GetProductsImages(int productId)
    {
        return _service.GetAllProductsImages(productId);
    }
    
}