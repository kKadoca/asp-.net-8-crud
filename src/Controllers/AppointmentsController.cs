using BarbershopApi.DTOs;
using BarbershopApi.Exceptions;
using BarbershopApi.Models;
using BarbershopApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BarbershopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentService _service;

    public AppointmentsController(IAppointmentService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var appointments = await _service.GetAllAsync();
        return Ok(appointments.Select(ToResponse));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var appointment = await _service.GetByIdAsync(id);
        if (appointment is null) return NotFound();
        return Ok(ToResponse(appointment));
    }

    [HttpGet("count")]
    public async Task<IActionResult> GetCount()
    {
        var count = await _service.GetCountAsync();
        return Ok(new { count });
    }

    [HttpGet("search")]
    public async Task<IActionResult> GetByStatus([FromQuery] string status)
    {
        if (!Enum.TryParse<AppointmentStatus>(status, ignoreCase: true, out var parsedStatus))
            throw new BusinessRuleException($"Invalid status '{status}'. Valid values: Scheduled, Completed, Cancelled, NoShow.");

        var appointments = await _service.GetByStatusAsync(parsedStatus);
        return Ok(appointments.Select(ToResponse));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAppointmentRequest request)
    {
        var appointment = new Appointment
        {
            DateTime = request.DateTime,
            Status = AppointmentStatus.Scheduled,
            CustomerId = request.CustomerId,
            ProfessionalId = request.ProfessionalId,
            PrestationOfServiceId = request.PrestationOfServiceId
        };
        await _service.CreateAsync(appointment);

        var created = await _service.GetByIdAsync(appointment.Id);
        return CreatedAtAction(nameof(GetById), new { id = appointment.Id }, ToResponse(created!));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAppointmentRequest request)
    {
        if (!Enum.TryParse<AppointmentStatus>(request.Status, ignoreCase: true, out var parsedStatus))
            throw new BusinessRuleException($"Invalid status '{request.Status}'. Valid values: Scheduled, Completed, Cancelled, NoShow.");

        var appointment = new Appointment
        {
            Id = id,
            DateTime = request.DateTime,
            Status = parsedStatus,
            CustomerId = request.CustomerId,
            ProfessionalId = request.ProfessionalId,
            PrestationOfServiceId = request.PrestationOfServiceId
        };
        await _service.UpdateAsync(appointment);

        var updated = await _service.GetByIdAsync(id);
        return Ok(ToResponse(updated!));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    private static AppointmentResponse ToResponse(Appointment a) => new(
        a.Id,
        a.DateTime,
        a.Status.ToString(),
        new CustomerResponse(a.Customer.Id, a.Customer.Name, a.Customer.Email, a.Customer.Phone),
        new ProfessionalResponse(a.Professional.Id, a.Professional.Name, a.Professional.Phone),
        new PrestationOfServiceResponse(a.PrestationOfService.Id, a.PrestationOfService.Name, a.PrestationOfService.Description, a.PrestationOfService.Price, a.PrestationOfService.DurationMinutes)
    );
}
