﻿using infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using service.Services;

namespace api.Controllers;

public class ProductController : ControllerBase
{
            private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("api/GetAllProducts")]
        public IEnumerable<Products> getAllProducts()
        {
            return _productService.getAllProducts();
        }

        [HttpGet]
        [Route("/api/product/GetProductById")]
        public ActionResult<Products> GetProductById(int productId)
        {
            try
            {
                var product = _productService.getProductById(productId);

                if (product == null)
                {
                    return NotFound();
                }

                return Ok(product);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("/api/product/createProduct")]
        public ActionResult CreateProduct([FromBody] Products product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest("Invalid product data");
                }
                

                return Ok(_productService.createProduct(product));
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("/api/product/updateProduct")]
        public ActionResult UpdateProduct(int productId, [FromBody] Products product)
        {
            try
            {
                if (product == null || productId != product.ProductId)
                {
                    return BadRequest("Invalid product data");
                }

                var existingProduct = _productService.getProductById(productId);

                if (existingProduct == null)
                {
                    return NotFound();
                }

                _productService.updateProduct(product);

                return Ok(product);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("/api/product/deleteProduct")]
        public ActionResult DeleteProduct(int productId)
        {
            try
            {
                var existingProduct = _productService.getProductById(productId);

                if (existingProduct == null)
                {
                    return NotFound();
                }

                _productService.deleteProduct(productId);

                return Ok(productId);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("/api/product/type/GetProducstByTypeId")]
        public ActionResult<List<Products>> GetProductsByType(int typeId)
        {
            try
            {
                var products = _productService.getProductByType(typeId);
                return Ok(products);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("/api/product/gender/GetProductsByGender")]
        public ActionResult<List<Products>> GetProductsByGender(string gender)
        {
            try
            {
                var products = _productService.getProductByGender(gender);
                return Ok(products);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("api/product/type/gender/GetProductsByGenderType")]
        public ActionResult<List<Products>> GetProductsByGenderType(int typeId, string gender)
        {
            try
            {
                var products = _productService.getProductByGenderType(typeId, gender);
                return Ok(products);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
}
