using BarbershopApi.Exceptions;
using BarbershopApi.Models;
using BarbershopApi.Repositories.Interfaces;
using BarbershopApi.Services.Interfaces;
using BarbershopApi.States.Appointment;

namespace BarbershopApi.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _repository;

    public AppointmentService(IAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Appointment>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Appointment?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Appointment>> GetByStatusAsync(AppointmentStatus status)
    {
        return await _repository.GetByStatusAsync(status);
    }

    public async Task<int> GetCountAsync()
    {
        return await _repository.GetCountAsync();
    }

    public async Task CreateAsync(Appointment appointment)
    {
        ValidateForeignKeys(appointment);
        AppointmentState.For(appointment.Status).ValidateDateTime(appointment.DateTime);
        await _repository.AddAsync(appointment);
    }

    public async Task UpdateAsync(Appointment appointment)
    {
        var existing = await _repository.GetByIdAsync(appointment.Id)
            ?? throw new NotFoundException(nameof(Appointment), appointment.Id);

        ValidateForeignKeys(appointment);

        var currentState = AppointmentState.For(existing.Status);

        if (existing.Status != appointment.Status)
            currentState.EnsureCanTransitionTo(appointment.Status);

        AppointmentState.For(appointment.Status).ValidateDateTime(appointment.DateTime);

        await _repository.UpdateAsync(appointment);
    }

    public async Task DeleteAsync(int id)
    {
        _ = await _repository.GetByIdAsync(id)
            ?? throw new NotFoundException(nameof(Appointment), id);

        await _repository.DeleteAsync(id);
    }

    private static void ValidateForeignKeys(Appointment appointment)
    {
        if (appointment.CustomerId <= 0)
            throw new BusinessRuleException("A customer is required to create/update an appointment.");

        if (appointment.ProfessionalId <= 0)
            throw new BusinessRuleException("A professional is required to create/update an appointment.");

        if (appointment.PrestationOfServiceId <= 0)
            throw new BusinessRuleException("A service is required to create/update an appointment.");
    }
}
