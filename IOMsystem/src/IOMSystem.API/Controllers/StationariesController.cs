using IOMSystem.Application.DTOs;
using IOMSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IOMSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StationariesController : ControllerBase
{
    private readonly IStationaryService _service;

    public StationariesController(IStationaryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await _service.GetAllStationariesAsync();
        return Ok(list);
    }

    [HttpGet("{code}")]
    public async Task<IActionResult> GetByCode(string code)
    {
        var item = await _service.GetStationaryByCodeAsync(code);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] StationaryDto dto)
    {
        var success = await _service.CreateStationaryAsync(dto);
        if (!success) return BadRequest("Stationary already exists or invalid data.");
        return CreatedAtAction(nameof(GetByCode), new { code = dto.LocationCode }, dto);
    }

    [HttpPut("{code}")]
    public async Task<IActionResult> Update(string code, [FromBody] StationaryDto dto)
    {
        if (code != dto.LocationCode) return BadRequest("Code mismatch");
        var success = await _service.UpdateStationaryAsync(code, dto);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpDelete("{code}")]
    public async Task<IActionResult> Delete(string code)
    {
        var success = await _service.DeleteStationaryAsync(code);
        if (!success) return NotFound();
        return NoContent();
    }
}
