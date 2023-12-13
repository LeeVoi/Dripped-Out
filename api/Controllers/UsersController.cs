using infrastructure.Entities;
using infrastructure.Entities.Helper;
using infrastructure.Repositories.Factory;
using infrastructure.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    
    private readonly ICrud<Users> _userRepository;

    public UsersController(CRUDFactory crudFactory)
    {
        _userRepository = crudFactory.GetRepository<Users>(RepoType.UserRepo);
    }

    [HttpPost("/GetUser")]
    public IActionResult Get(int id)
    {
        var user = _userRepository.Read(id);
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

        _userRepository.Create(user);

        // Instead of CreatedAtRoute, return the user directly
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

        _userRepository.Update(user);
        return Ok(user);
    }

    // DELETE: api/User/5
    [HttpDelete("/Delete")]
    public IActionResult Delete(int id)
    {
        var user = _userRepository.Read(id);
        if (user == null)
        {
            return NotFound();
        }
        _userRepository.Delete(id);
        return Ok(id);
    }
    
}