using BarbershopApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BarbershopApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Professional> Professionals => Set<Professional>();
    public DbSet<PrestationOfService> PrestationsOfService => Set<PrestationOfService>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
}
