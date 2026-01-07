using IOMSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using IOMSystem.Contract.DTOs;

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

    [HttpGet("{code}")]
    public async Task<IActionResult> GetById(string code)
    {
        var branch = await _branchService.GetBranchByIdAsync(code);
        if (branch == null) return NotFound();
        return Ok(branch);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBranchDto branchDto)
    {
        await _branchService.CreateBranchAsync(branchDto);
        return CreatedAtAction(nameof(GetById), new { code = branchDto.BranchCode }, branchDto);
    }

    [HttpPut("{code}")]
    public async Task<IActionResult> Update(string code, [FromBody] UpdateBranchDto branchDto)
    {
        var result = await _branchService.UpdateBranchAsync(code, branchDto);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpPatch("{code}/status")]
    public async Task<IActionResult> UpdateStatus(string code, [FromBody] BranchStatusDto statusDto)
    {
        var result = await _branchService.UpdateBranchStatusAsync(code, statusDto.IsActive);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpDelete("{code}")]
    public async Task<IActionResult> Delete(string code)
    {
        await _branchService.DeleteBranchAsync(code);
        return NoContent();
    }
}
