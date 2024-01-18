using infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using service.Services.Interface;

namespace api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("/GetUser")]
    public IActionResult Get(int id)
    {
        try
        {
            var user = _userService.getUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
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
    [Route("api/UserController/create")]    
    public IActionResult Create([FromBody] Users user)
    {
        try
        {
            if (user == null)
            {
                return BadRequest();
            }

            _userService.createUser(user);
            return Ok(user);
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

    
    [HttpPut("/UpdateUser")]
    public IActionResult Update(int id, [FromBody] Users user)
    {
        try
        {
            if (user == null || user.UserId != id)
            {
                return BadRequest();
            }

            _userService.updateUser(user);
            return Ok(user);
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

    
    [HttpDelete("/Delete")]
    public IActionResult Delete(int id)
    {
        try
        {
            var user = _userService.getUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            _userService.deleteUser(id);
            return Ok(id);
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