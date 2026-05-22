using BarbershopApi.Data;
using BarbershopApi.Models;
using BarbershopApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BarbershopApi.Repositories;

public class PrestationOfServiceRepository : IPrestationOfServiceRepository
{
    private readonly AppDbContext _context;

    public PrestationOfServiceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PrestationOfService>> GetAllAsync()
    {
        return await _context.PrestationsOfService.ToListAsync();
    }

    public async Task<PrestationOfService?> GetByIdAsync(int id)
    {
        return await _context.PrestationsOfService
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<PrestationOfService>> GetByNameAsync(string name)
    {
        return await _context.PrestationsOfService
            .Where(p => p.Name.Contains(name))
            .ToListAsync();
    }

    public async Task<int> GetCountAsync()
    {
        return await _context.PrestationsOfService.CountAsync();
    }

    public async Task<bool> ExistsByNameAsync(string name, int? excludeId = null)
    {
        return await _context.PrestationsOfService
            .AnyAsync(p => p.Name == name && (excludeId == null || p.Id != excludeId));
    }

    public async Task AddAsync(PrestationOfService prestationOfService)
    {
        await _context.PrestationsOfService.AddAsync(prestationOfService);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(PrestationOfService prestationOfService)
    {
        _context.PrestationsOfService.Update(prestationOfService);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var prestationOfService = await _context.PrestationsOfService.FindAsync(id);
        if (prestationOfService is not null)
        {
            prestationOfService.IsDeleted = true;
            prestationOfService.DeletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }
}
