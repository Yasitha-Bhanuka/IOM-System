using IOMSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using IOMSystem.Contract.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace IOMSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RegistrationsController : ControllerBase
{
    private readonly IRegistrationService _service;

    public RegistrationsController(IRegistrationService service)
    {
        _service = service;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRegistrationRequestDto dto)
    {
        var success = await _service.CreateRequestAsync(dto);
        if (!success) return BadRequest("Request failed. Email might exist.");
        return Ok("Request created.");
    }

    [Authorize(Roles = "Manager")]
    [HttpGet("pending")]
    public async Task<IActionResult> GetPending()
    {
        var list = await _service.GetPendingRequestsAsync();
        return Ok(list);
    }

    [Authorize(Roles = "Manager")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await _service.GetAllRequestsAsync();
        return Ok(list);
    }

    [Authorize(Roles = "Manager")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var request = await _service.GetRequestByIdAsync(id);
        if (request == null) return NotFound();
        return Ok(request);
    }


    [Authorize(Roles = "Manager")]
    [HttpPost("approve/{id}")]
    public async Task<IActionResult> Approve(int id, [FromBody] int approvedByUserId)
    {
        var success = await _service.ApproveRequestAsync(id, approvedByUserId);
        if (!success) return BadRequest("Approval failed.");
        return Ok("Approved.");
    }

    [Authorize(Roles = "Manager")]
    [HttpPost("reject/{id}")]
    public async Task<IActionResult> Reject(int id, [FromBody] RejectRegistrationDto actionDto)
    {
        var success = await _service.RejectRequestAsync(id, actionDto.ActionByUserId, actionDto.RejectionReason);
        if (!success) return BadRequest("Rejection failed.");
        return Ok("Rejected.");
    }

    [Authorize(Roles = "Manager")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.DeleteRequestAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}
