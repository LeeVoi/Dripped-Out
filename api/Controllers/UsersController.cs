using infrastructure.Entities;
using infrastructure.Entities.Helper;
using infrastructure.Repositories.Factory;
using infrastructure.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using service.Services;

namespace api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("/GetUser")]
    public IActionResult Get(int id)
    {
        var user = _userService.getUserById(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost]
    [Route("api/UserController/create")]    
    public IActionResult Create([FromBody] Users user)
    {
        if (user == null)
        {
            return BadRequest();
        }

        _userService.createUser(user);
        
        return Ok(user);
    }

    // PUT: api/User/5
    [HttpPut("/UpdateUser")]
    public IActionResult Update(int id, [FromBody] Users user)
    {
        if (user == null || user.UserId != id)
        {
            return BadRequest();
        }

        _userService.updateUser(user);
        return Ok(user);
    }

    // DELETE: api/User/5
    [HttpDelete("/Delete")]
    public IActionResult Delete(int id)
    {
        var user = _userService.getUserById(id);
        if (user == null)
        {
            return NotFound();
        }
        _userService.deleteUser(id);
        return Ok(id);
    }
    
}