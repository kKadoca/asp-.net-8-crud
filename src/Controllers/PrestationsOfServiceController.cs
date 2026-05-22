using BarbershopApi.DTOs;
using BarbershopApi.Models;
using BarbershopApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BarbershopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrestationsOfServiceController : ControllerBase
{
    private readonly IPrestationOfServiceService _service;

    public PrestationsOfServiceController(IPrestationOfServiceService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var services = await _service.GetAllAsync();
        return Ok(services.Select(ToResponse));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var service = await _service.GetByIdAsync(id);
        if (service is null) return NotFound();
        return Ok(ToResponse(service));
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
        var services = await _service.GetByNameAsync(name);
        return Ok(services.Select(ToResponse));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePrestationOfServiceRequest request)
    {
        var prestationOfService = new PrestationOfService
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            DurationMinutes = request.DurationMinutes
        };
        await _service.CreateAsync(prestationOfService);
        return CreatedAtAction(nameof(GetById), new { id = prestationOfService.Id }, ToResponse(prestationOfService));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePrestationOfServiceRequest request)
    {
        var prestationOfService = new PrestationOfService
        {
            Id = id,
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            DurationMinutes = request.DurationMinutes
        };
        await _service.UpdateAsync(prestationOfService);
        return Ok(ToResponse(prestationOfService));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    private static PrestationOfServiceResponse ToResponse(PrestationOfService p) =>
        new(p.Id, p.Name, p.Description, p.Price, p.DurationMinutes);
}
