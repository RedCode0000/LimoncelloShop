using LimoncelloShop.Domain.Interfaces;
using LimoncelloShop.Domain.Objects;
using Microsoft.AspNetCore.Mvc;

namespace LimoncelloShop.Api.Controllers;

//[Authorize]
[ApiController]
[Route("Limoncello")]
public class LimoncelloController : Controller
{

    private readonly ILimoncelloService _LimoncelloService;

    public LimoncelloController(ILimoncelloService LimoncelloService)
    {
        _LimoncelloService = LimoncelloService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _LimoncelloService.GetAllLimoncello();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var drink = _LimoncelloService.GetLimoncello(id);
        return Ok(drink);
    }

    //[Authorize(Roles = "Admin")]
    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> Create([FromBody] LimoncelloDTO model)
    {
        try
        {
            model.Id = 0;
            var Limoncello = LimoncelloDTO.ToLimoncello(model);
            await _LimoncelloService.AddLimoncello(Limoncello);
            return Ok("Done");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    //[Authorize(Roles = "Admin")]
    [HttpPut]
    [Route("Edit")]
    public async Task<IActionResult> Edit([FromBody] LimoncelloDTO model)
    {
        try
        {
            Limoncello? existingLimoncello = _LimoncelloService.GetLimoncello(model.Id);
            if (existingLimoncello == null)
                throw new ArgumentException($"The ticket to edit with id {model.Id} does not exist.");
            await _LimoncelloService.EditLimoncello(existingLimoncello, model);
            return Ok(existingLimoncello);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    //[Authorize(Roles = "Admin")]
    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<IActionResult> DeleteTicket(int id)
    {
        try
        {
            Limoncello? existingLimoncello = _LimoncelloService.GetLimoncello(id);
            if (existingLimoncello == null)
                throw new ArgumentException($"The ticket to delete with id {id} does not exist.");
            await _LimoncelloService.DeleteLimoncello(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}
