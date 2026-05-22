using BarbershopApi.Data;
using BarbershopApi.Models;
using BarbershopApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BarbershopApi.Repositories;

public class ProfessionalRepository : IProfessionalRepository
{
    private readonly AppDbContext _context;

    public ProfessionalRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Professional>> GetAllAsync()
    {
        return await _context.Professionals.ToListAsync();
    }

    public async Task<Professional?> GetByIdAsync(int id)
    {
        return await _context.Professionals
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Professional>> GetByNameAsync(string name)
    {
        return await _context.Professionals
            .Where(p => p.Name.Contains(name))
            .ToListAsync();
    }

    public async Task<int> GetCountAsync()
    {
        return await _context.Professionals.CountAsync();
    }

    public async Task<bool> ExistsByPhoneAsync(string phone, int? excludeId = null)
    {
        return await _context.Professionals
            .AnyAsync(p => p.Phone == phone && (excludeId == null || p.Id != excludeId));
    }

    public async Task AddAsync(Professional professional)
    {
        await _context.Professionals.AddAsync(professional);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Professional professional)
    {
        _context.Professionals.Update(professional);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var professional = await _context.Professionals.FindAsync(id);
        if (professional is not null)
        {
            _context.Professionals.Remove(professional);
            await _context.SaveChangesAsync();
        }
    }
}
