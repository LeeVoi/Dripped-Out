﻿using System.Text;
 using api.Authorizers;
 using infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using service.Helpers;
using service.Services;

namespace api.Controllers;

public class ProductController : ControllerBase
{
            private readonly ProductService _productService;
            private readonly AuthenticationHelper _authentication;

        public ProductController(ProductService productService, AuthenticationHelper authenticationHelper)
        {
            _productService = productService;
            _authentication = authenticationHelper;
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

        [AuthorizeAdmin]
        [HttpPost]
        [Route("/api/product/createProduct")]
        public ActionResult<Products> CreateProduct([FromBody] Products product)
        {
            string token = null;

            if(Request.Headers.ContainsKey("Authorization"))
                token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (token == null)
                return Unauthorized("Please log in to be authorized");

            if (!_authentication.ValidateCurrentToken(token))
                return Unauthorized("This is fishy");

            var payload = _authentication.ExtractPayloadFromToken(token);
            var isAdmin = bool.Parse(payload["isadmin"].ToString());

            if (!isAdmin)
                return Forbid("You do not have access to these controls");
            
            try
            {
                if (product == null)
                {
                    return BadRequest("Invalid product data");
                }
                var createdproduct = _productService.createProduct(product);

                return Ok(createdproduct);
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

        [AuthorizeAdmin]
        [HttpPut]
        [Route("/api/product/updateProduct")]
        public ActionResult UpdateProduct(int productId, [FromBody] Products product)
        {
            string token = null;

            if(Request.Headers.ContainsKey("Authorization"))
                token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (token == null)
                return Unauthorized("Please log in to be authorized");

            if (!_authentication.ValidateCurrentToken(token))
                return Unauthorized("This is fishy");

            var payload = _authentication.ExtractPayloadFromToken(token);
            var isAdmin = bool.Parse(payload["isadmin"].ToString());

            if (!isAdmin)
                return Forbid("You do not have access to these controls");
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

        [AuthorizeAdmin]
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
