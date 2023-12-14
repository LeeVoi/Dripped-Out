using infrastructure.Entities;
using infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using service.Services;

namespace api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class UserCartController : ControllerBase
{
    private readonly UserProdService _userProdService;

    public UserCartController(UserProdService userProdService)
    {
        _userProdService = userProdService;
    }
    
    [HttpPost("/GetUserLikes")]
    public IActionResult GetUserLikes(int userId)
    {
        var productsList = _userProdService.GetUserLikes(userId);
        
        if (productsList == null)
        {
            return NotFound();
        }
        return Ok(productsList);
    }
    
    [HttpPost("/GetUserCart")]
    public IActionResult GetUserCart(int id)
    {
        var productsList = _userProdService.GetUserCart(id);
        if (productsList == null)
        {
            return NotFound();
        }
        return Ok(productsList);
    }

    [HttpPost]
    [Route("AddProductToUserCart")]
    public ActionResult AddProductToUserCart([FromBody] UserCart userCart)
    {
        try
        {
            if (userCart == null)
            {
                return BadRequest("Invalid request data");
            }

            int userId = userCart.UserId;
            int productId =userCart.ProductId;

            _userProdService.AddProductToUserCart(userId, productId);

            return CreatedAtAction(nameof(GetUserCart), new { userId = userId }, userCart);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "An error occurred while processing the request." });
        }
    }
    
    [HttpPost]
    [Route("AddProductToUserLikes")]
    public ActionResult AddProductToUserLikes([FromBody] UserLikes userLikes)
    {
        try
        {
            if (userLikes == null)
            {
                return BadRequest("Invalid request data");
            }

            int userId = userLikes.UserId;
            int productId = userLikes.ProductId;

            _userProdService.AddProductToUserLikes(userId, productId);

            return CreatedAtAction(nameof(GetUserLikes), new { userId = userId }, userLikes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "An error occurred while processing the request." });
        }
    }
}