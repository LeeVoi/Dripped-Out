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
    
    [HttpGet("/GetUserLikes")]
    public IActionResult GetUserLikes(int userId)
    {
        var productsList = _userProdService.GetUserLikes(userId);
        
        if (productsList == null)
        {
            return NotFound();
        }
        return Ok(productsList);
    }
    
    [HttpGet("/GetUserCart")]
    public IActionResult GetUserCart(int id)
    {
        var productsList = _userProdService.GetUserCart(id);
        if (productsList == null)
        {
            return NotFound();
        }
        return Ok(productsList);
    }
}