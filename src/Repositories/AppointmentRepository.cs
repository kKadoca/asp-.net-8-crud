using BarbershopApi.Data;
using BarbershopApi.Models;
using BarbershopApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BarbershopApi.Repositories;

public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
{
    public AppointmentRepository(AppDbContext context) : base(context) { }

    protected override DbSet<Appointment> DbSet => Context.Appointments;

    public override async Task<IEnumerable<Appointment>> GetAllAsync() =>
        await DbSet
            .Include(a => a.Customer)
            .Include(a => a.Professional)
            .Include(a => a.PrestationOfService)
            .ToListAsync();

    public async Task<Appointment?> GetByIdAsync(int id) =>
        await DbSet
            .Include(a => a.Customer)
            .Include(a => a.Professional)
            .Include(a => a.PrestationOfService)
            .FirstOrDefaultAsync(a => a.Id == id);

    public async Task<IEnumerable<Appointment>> GetByStatusAsync(AppointmentStatus status) =>
        await DbSet
            .Include(a => a.Customer)
            .Include(a => a.Professional)
            .Include(a => a.PrestationOfService)
            .Where(a => a.Status == status)
            .ToListAsync();
}
