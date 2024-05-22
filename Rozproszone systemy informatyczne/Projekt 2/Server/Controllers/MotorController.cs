using Microsoft.AspNetCore.Mvc;
using Server.Contracts;
using Server.Models;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MotorController : ControllerBase
{
    private readonly IMotorService _motorService;

    public MotorController(IMotorService motorService) => _motorService = motorService;

    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Get() => Ok(await _motorService.GetAll());

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Detail(int id) => Ok(await _motorService.Detail(id));

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Post([FromBody] CreateMotor motor)
    {
        await _motorService.Create(motor);
        return Created();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(int id)
    {
        await _motorService.Remove(id);
        return NoContent();
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Put(UpdateMotor motor)
    {
        await _motorService.Update(motor);
        return Accepted();
    }

    [HttpGet("find/{search}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Find(string search) => Ok(await _motorService.GetSelected(search));

    [HttpPost("reserve/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Reserve(int id)
    {
        await _motorService.Reserve(id);
        return NoContent();
    }

    [HttpPost("cancelreserve/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CancelReserve(int id)
    {
        await _motorService.CancelReserve(id);
        return NoContent();
    }

    [HttpPost("rent")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Reserve(int id, int numOfDays)
    {
        await _motorService.Rent(id, numOfDays);
        return NoContent();
    }

    [HttpGet("pdf/{id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> PDF(int id) => File(await _motorService.GeneratePDF(id), "application/octet-stream", "file.pdf");
}
