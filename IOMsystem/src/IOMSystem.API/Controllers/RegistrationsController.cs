using IOMSystem.Application.DTOs;
using IOMSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IOMSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegistrationsController : ControllerBase
{
    private readonly IRegistrationService _service;

    public RegistrationsController(IRegistrationService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RegistrationRequestDto dto)
    {
        var success = await _service.CreateRequestAsync(dto);
        if (!success) return BadRequest("Request failed. Email might exist.");
        return Ok("Request created.");
    }

    [HttpGet("pending")]
    public async Task<IActionResult> GetPending()
    {
        var list = await _service.GetPendingRequestsAsync();
        return Ok(list);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await _service.GetAllRequestsAsync();
        return Ok(list);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        // Not directly exposed in Service yet? Service returns List. 
        // Need to add GetById to Service Interface/Impl if strict.
        // Actually Service doesn't have GetById exposed in Interface.
        // Let's rely on GetAll/Pending for now or quick patch service.
        // Wait, repository has it.
        // I will add GetById to Service first.
        return NotFound("Not implemented yet");
    }


    [HttpPost("approve/{id}")]
    public async Task<IActionResult> Approve(int id, [FromBody] int approvedByUserId)
    {
        var success = await _service.ApproveRequestAsync(id, approvedByUserId);
        if (!success) return BadRequest("Approval failed.");
        return Ok("Approved.");
    }

    [HttpPost("reject/{id}")]
    public async Task<IActionResult> Reject(int id, [FromBody] RegistrationRequestDto actionDto)
    {
        // Expecting ActionByUserId and RejectionReason in DTO
        if (actionDto.ActionByUserId == null) return BadRequest("Admin ID required.");

        var success = await _service.RejectRequestAsync(id, actionDto.ActionByUserId.Value, actionDto.RejectionReason);
        if (!success) return BadRequest("Rejection failed.");
        return Ok("Rejected.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.DeleteRequestAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}
