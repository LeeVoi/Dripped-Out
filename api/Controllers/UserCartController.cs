using api.Authorizers;
using infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using service.Services;

namespace api.Controllers;

[AuthorizeUserId]
[Route("api/[controller]")]
[ApiController]
public class UserCartController : ControllerBase
{
    private readonly UserProdService _userProdService;

    public UserCartController(UserProdService userProdService)
    {
        _userProdService = userProdService;
    }
    
    [HttpGet("/GetUserLikes")]
    public IActionResult GetUserLikes()
    {
        var userIdString = HttpContext.Items["userId"].ToString();

        int userId = 0;
        if (userIdString != null)
        {
            Int32.TryParse(userIdString, out userId);
        }
        try
        {

            var productsList = _userProdService.GetUserLikes(userId);
        
            if (productsList == null)
            {
                return NotFound();
            }
            return Ok(productsList);

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
    
    [HttpGet("/GetUserCartProducts")]
    public IActionResult GetUserCartProducts()
    {
        var userIdString = HttpContext.Items["userId"].ToString();

        int userId = 0;
        if (userIdString != null)
        {
            Int32.TryParse(userIdString, out userId);
        }
        try
        {

            var productsList = _userProdService.GetUserCartProducts(userId);
            if (productsList == null)
            {
                return NotFound();
            }
            return Ok(productsList);

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
    [Route("GetUserCart")]
    public ActionResult GetUserCartDetails()
    {
        var userIdString = HttpContext.Items["userId"].ToString();

        int userId = 0;
        if (userIdString != null)
        {
            Int32.TryParse(userIdString, out userId);
        }
        try
        {
            var productsList = _userProdService.GetUserCartDetails(userId);
            if (productsList == null)
            {
                return NotFound();
            }
            return Ok(productsList);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    [Route("AddProductToUserCart")]
    public ActionResult AddProductToUserCart([FromBody] UserCart userCart)
    {
        var userIdString = HttpContext.Items["userId"].ToString();

        int userId = 0;
        if (userIdString != null)
        {
            Int32.TryParse(userIdString, out userId);
        }
        try
        {
            if (userCart == null)
            {
                return BadRequest("Invalid request data");
            }
            int productId = userCart.ProductId;
            int colorId = userCart.ColorId;
            int sizeId = userCart.SizeId;
            int quantity = userCart.Quantity;

            _userProdService.AddProductToUserCart(userId, productId, colorId, sizeId, quantity);

            return CreatedAtAction(nameof(GetUserCartProducts), new { userId = userId });
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
    [Route("RemoveProductFromCart")]
    public IActionResult RemoveProductFromCart(int productId, int colorId, int sizeId, int quantity)
    {
        var userIdString = HttpContext.Items["userId"].ToString();

        int userId = 0;
        if (userIdString != null)
        {
            Int32.TryParse(userIdString, out userId);
        }
        try
        {
            _userProdService.RemoveProductFromCart(userId, productId, colorId, sizeId, quantity);
            return Ok("Product removed from cart successfully.");
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
    [Route("UpdateProductQuantityInCart")]
    public IActionResult UpdateProductQuantityInCart(int productId, int colorId, int sizeId, int newQuantity)
    {
        var userIdString = HttpContext.Items["userId"].ToString();

        int userId = 0;
        if (userIdString != null)
        {
            Int32.TryParse(userIdString, out userId);
        }
        try
        {
            _userProdService.UpdateProductQuantity(userId, productId, colorId, sizeId, newQuantity);
            return Ok("Product quantity updated successfully.");
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
    [Route("AddProductToUserLikes")]
    public ActionResult AddProductToUserLikes([FromBody] UserLikes userLikes)
    {
        var userIdString = HttpContext.Items["userId"].ToString();

        int userId = 0;
        if (userIdString != null)
        {
            Int32.TryParse(userIdString, out userId);
        }
        try
        {
            if (userLikes == null)
            {
                return BadRequest("Invalid request data");
            }
            
            int productId = userLikes.ProductId;

            _userProdService.AddProductToUserLikes(userId, productId);

            return CreatedAtAction(nameof(GetUserLikes), new { userId = userId }, userLikes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "An error occurred while processing the request." });
        }
    }
    [HttpDelete]
    [Route("RemoveProductFromLiked")]
    public IActionResult RemoveProductFromLikes(int productId)
    {
        var userIdString = HttpContext.Items["userId"].ToString();

        int userId = 0;
        if (userIdString != null)
        {
            Int32.TryParse(userIdString, out userId);
        }
        try
        {
            _userProdService.RemoveProductFromLikes(userId, productId);
            return Ok("Product removed from liked successfully.");
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
