using BarbershopApi.DTOs;
using BarbershopApi.Models;
using BarbershopApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BarbershopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfessionalsController : ControllerBase
{
    private readonly IProfessionalService _service;

    public ProfessionalsController(IProfessionalService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var professionals = await _service.GetAllAsync();
        return Ok(professionals.Select(ToResponse));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var professional = await _service.GetByIdAsync(id);
        if (professional is null) return NotFound();
        return Ok(ToResponse(professional));
    }

    [HttpGet("count")]
    public async Task<IActionResult> GetCount()
    {
        var count = await _service.GetCountAsync();
        return Ok(new { count });
    }

    [HttpGet("search")]
    public async Task<IActionResult> GetByName([FromQuery] string name)
    {
        var professionals = await _service.GetByNameAsync(name);
        return Ok(professionals.Select(ToResponse));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProfessionalRequest request)
    {
        var professional = new Professional { Name = request.Name, Phone = request.Phone };
        await _service.CreateAsync(professional);
        return CreatedAtAction(nameof(GetById), new { id = professional.Id }, ToResponse(professional));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProfessionalRequest request)
    {
        var professional = new Professional { Id = id, Name = request.Name, Phone = request.Phone };
        await _service.UpdateAsync(professional);
        return Ok(ToResponse(professional));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    private static ProfessionalResponse ToResponse(Professional p) => new(p.Id, p.Name, p.Phone);
}
