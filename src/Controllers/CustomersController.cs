using BarbershopApi.DTOs;
using BarbershopApi.Models;
using BarbershopApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BarbershopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _service;

    public CustomersController(ICustomerService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customers = await _service.GetAllAsync();
        return Ok(customers.Select(ToResponse));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var customer = await _service.GetByIdAsync(id);
        if (customer is null) return NotFound();
        return Ok(ToResponse(customer));
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
        var customers = await _service.GetByNameAsync(name);
        return Ok(customers.Select(ToResponse));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerRequest request)
    {
        var customer = new Customer { Name = request.Name, Email = request.Email, Phone = request.Phone };
        await _service.CreateAsync(customer);
        return CreatedAtAction(nameof(GetById), new { id = customer.Id }, ToResponse(customer));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCustomerRequest request)
    {
        var customer = new Customer { Id = id, Name = request.Name, Email = request.Email, Phone = request.Phone };
        await _service.UpdateAsync(customer);
        return Ok(ToResponse(customer));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    private static CustomerResponse ToResponse(Customer c) => new(c.Id, c.Name, c.Email, c.Phone);
}
