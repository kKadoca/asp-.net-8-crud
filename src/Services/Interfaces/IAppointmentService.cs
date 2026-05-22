using BarbershopApi.Models;

namespace BarbershopApi.Services.Interfaces;

public interface IAppointmentService
{
    Task<IEnumerable<Appointment>> GetAllAsync();
    Task<Appointment?> GetByIdAsync(int id);
    Task<IEnumerable<Appointment>> GetByStatusAsync(AppointmentStatus status);
    Task<int> GetCountAsync();
    Task CreateAsync(Appointment appointment);
    Task UpdateAsync(Appointment appointment);
    Task DeleteAsync(int id);
}
