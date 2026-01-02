using IOMSystem.Application.DTOs;
using IOMSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IOMSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BranchesController : ControllerBase
{
    private readonly IBranchService _branchService;

    public BranchesController(IBranchService branchService)
    {
        _branchService = branchService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var branches = await _branchService.GetAllBranchesAsync();
        return Ok(branches);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var branch = await _branchService.GetBranchByIdAsync(id);
        if (branch == null) return NotFound();
        return Ok(branch);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BranchDto branchDto)
    {
        await _branchService.CreateBranchAsync(branchDto);
        return CreatedAtAction(nameof(GetById), new { id = branchDto.BranchId }, branchDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] BranchDto branchDto)
    {
        if (id != branchDto.BranchId && branchDto.BranchId != 0) return BadRequest();

        var result = await _branchService.UpdateBranchAsync(id, branchDto);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _branchService.DeleteBranchAsync(id);
        return NoContent();
    }
}
