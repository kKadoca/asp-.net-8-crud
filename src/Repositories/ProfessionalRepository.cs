using BarbershopApi.Data;
using BarbershopApi.Models;
using BarbershopApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BarbershopApi.Repositories;

public class ProfessionalRepository : BaseRepository<Professional>, IProfessionalRepository
{
    public ProfessionalRepository(AppDbContext context) : base(context) { }

    protected override DbSet<Professional> DbSet => Context.Professionals;

    public async Task<Professional?> GetByIdAsync(int id) =>
        await DbSet.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

    public async Task<IEnumerable<Professional>> GetByNameAsync(string name) =>
        await DbSet.Where(p => p.Name.Contains(name)).ToListAsync();

    public async Task<bool> ExistsByPhoneAsync(string phone, int? excludeId = null) =>
        await DbSet.AnyAsync(p => p.Phone == phone && (excludeId == null || p.Id != excludeId));
}
