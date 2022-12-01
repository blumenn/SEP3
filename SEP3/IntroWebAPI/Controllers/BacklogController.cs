using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace IntroWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BacklogController: ControllerBase
{
    private readonly IBacklogLogic backlogLogic;

    public BacklogController(IBacklogLogic backlogLogic)
    {
        this.backlogLogic = backlogLogic;
    }
    
     [HttpPost]
    public async Task<ActionResult<Backlog>> CreateAsync([FromBody]BacklogCreationDto dto)
    {
        try
        {
            Backlog created = await backlogLogic.CreateAsync(dto);
            return Created($"/backlog/{created.name}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Backlog>>> GetAsync([FromQuery] string? userName, 
        [FromQuery] bool? completedStatus, [FromQuery] string? titleContains)
    {
        try
        {
            SearchBacklogParametersDto parameters = new(userName, completedStatus, titleContains);
            var todos = await backlogLogic.GetAsync(parameters);
            return Ok(todos);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch]
    public async Task<ActionResult> UpdateAsync([FromBody] BacklogUpdateDto dto)
    {
        try
        {
            await backlogLogic.UpdateAsync(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{name}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] string? name)
    {
        try
        {
            await backlogLogic.DeleteAsync(name);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{name}")]
    public async Task<ActionResult<BacklogBasicDto>> GetByName([FromRoute] string? name)
    {
        try
        {
            BacklogBasicDto result = await backlogLogic.GetByNameAsync(name);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

}