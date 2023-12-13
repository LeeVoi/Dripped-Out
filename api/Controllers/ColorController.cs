using infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using service.Services;

namespace api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ColorController : ControllerBase
{

    private readonly ColorService _colorService;

    public ColorController(ColorService colorService)
    {
        _colorService = colorService;
    }
    
    [HttpGet("GetColorById")]
    public IActionResult Get(int id)
    {
        try
        {
            var colorType = _colorService.GetColorById(id);
            if (colorType == null)
            {
                return NotFound();
            }

            return Ok(colorType);
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

    [HttpPost("CreateColor")]
    public IActionResult Create([FromBody] ColorType colorType)
    {
        try
        {
            if (colorType == null)
            {
                return BadRequest();
            }
            _colorService.CreateColor(colorType);
            return Ok(colorType);
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

    [HttpDelete("DeleteColor")]
    public IActionResult Delete(int id)
    {
        try
        {
            var colorType = _colorService.GetColorById(id);
            if (colorType == null)
            {
                return BadRequest();
            }

            _colorService.DeleteColor(id);
            return Ok(colorType);
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

    [HttpPut("AddColorsToProduct")]
    public IActionResult AddColorToProduct(int productId,[FromBody] List<int> colorIds)
    {
        try
        {
            // Get the existing colors for the product
            var existingColors = _colorService.GetColorsByProductId(productId);
            
            // Find the colors that do not exist yet
            var newColors = colorIds.Except(existingColors.Select(c => c.ColorId)).ToList();

            // If any of the new colors do not already exist 
            if (newColors.Any())
            {
                // Add the new colors
                _colorService.AddColorToProduct(productId, newColors);
            }

            return Ok(newColors);
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(e.Message); // 400 Bad Request 
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message); // 500 Internal Server Error
        }
    }

    [HttpDelete("RemoveColorsFromProduct")]
    public IActionResult DeleteColorsFromProduct(int productId, [FromBody] List<int> colorIds)
    {
        try
        {
            _colorService.DeleteColorsFromProduct(productId, colorIds);
            return Ok();
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(e.Message); // 400 Bad Request
        }
        catch (Exception ex)
        {
            return StatusCode(500,ex.Message); // 500 Internal Server Error
        } 
    }
    
    [HttpGet("GetColorsByProductId")]
    public IActionResult GetColorsByProductId(int productId)
    {
        try
        {
            var colors = _colorService.GetColorsByProductId(productId);
            if (colors == null || !colors.Any())
            {
                return NotFound();
            }

            return Ok(colors);
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(e.Message); // 400 Bad Request 
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message); // 500 Internal Server Error
        }
    }
    
    [HttpGet("GetAllColorTypes")]
    public IActionResult GetAllColorTypes()
    {
        try
        {
            var colors = _colorService.GetAllColorTypes();
            if (colors == null || !colors.Any())
            {
                return NotFound();
            }

            return Ok(colors);
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(e.Message); // 400 Bad Request 
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message); // 500 Internal Server Error
        }
    }


}