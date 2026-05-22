using BarbershopApi.Data;
using BarbershopApi.Models;
using BarbershopApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BarbershopApi.Repositories;

public class PrestationOfServiceRepository : BaseRepository<PrestationOfService>, IPrestationOfServiceRepository
{
    public PrestationOfServiceRepository(AppDbContext context) : base(context) { }

    protected override DbSet<PrestationOfService> DbSet => Context.PrestationsOfService;

    public async Task<PrestationOfService?> GetByIdAsync(int id) =>
        await DbSet.FirstOrDefaultAsync(p => p.Id == id);

    public async Task<IEnumerable<PrestationOfService>> GetByNameAsync(string name) =>
        await DbSet.Where(p => p.Name.Contains(name)).ToListAsync();

    public async Task<bool> ExistsByNameAsync(string name, int? excludeId = null) =>
        await DbSet.AnyAsync(p => p.Name == name && (excludeId == null || p.Id != excludeId));
}
