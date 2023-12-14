using infrastructure.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using service.Services;

namespace api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class SizeController : ControllerBase
{
    private readonly SizeService _sizeService;

    public SizeController(SizeService service)
    {
        _sizeService = service;
    }

    [HttpGet("GetSizeById")]
    public IActionResult Get(int id)
    {
        try
        {
            var sizeType = _sizeService.GetSizeById(id);
            if (sizeType == null)
            {
                return NotFound();
            }

            return Ok(sizeType);
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

    [HttpPost("CreateSizeType")]
    public IActionResult Create([FromBody] SizeType sizeType)
    {
        try
        {
            if (sizeType == null)
            {
                return BadRequest();
            }
            _sizeService.CreatSize(sizeType);
            return Ok(sizeType);

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

    [HttpDelete("DeleteSizeType")]
    public IActionResult Delete(int id)
    {
        try
        {
            var colorType = _sizeService.GetSizeById(id);
            if (colorType == null)
            {
                return BadRequest();
            }
            _sizeService.DeleteSize(id);
            return Ok(colorType);
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

    [HttpPut("AddSizesToProduct")]
    public IActionResult AddSizesToProduct(int productId, [FromBody] List<int> sizeIds)
    {
        try
        {
            // Get the existing sizes for the product
            var existingSizes = _sizeService.GetSizesByProductId(productId);
            
            // Find the sizes that do not exist yet
            var newSizes = sizeIds.Except(existingSizes.Select(s => s.SizeId)).ToList();

            // If any of the new sizes do not already exist 
            if (newSizes.Any())
            {
                // Add new sizes
                _sizeService.AddSizeToProduct(productId, newSizes);
            }

            return Ok(newSizes);

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

    [HttpDelete("RemoveSizesFromProduct")]
    public IActionResult DeleteSizesFromProduct(int productId, [FromBody] List<int> sizeIds)
    {
        try
        {
            
            _sizeService.RemoveSizeFromProduct(productId , sizeIds);
            return Ok();

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

    [HttpGet("GetSizesByProductId")]
    public IActionResult GetSizesByProductId(int productId)
    {
        try
        {

            var sizes = _sizeService.GetSizesByProductId(productId);
            if (sizes == null)
            {
                return NotFound();
            }

            return Ok(sizes);

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

    [HttpGet("GetAllSizeTypes")]
    public IActionResult GetAllSizeTypes()
    {
        try
        {

            var sizes = _sizeService.GetAllSizes();
            if (sizes == null)
            {
                return NotFound();
            }

            return Ok(sizes);

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