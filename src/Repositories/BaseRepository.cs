using BarbershopApi.Data;
using BarbershopApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BarbershopApi.Repositories;

public abstract class BaseRepository<T> where T : class, ISoftDeletable
{
    protected readonly AppDbContext Context;

    protected abstract DbSet<T> DbSet { get; }

    protected BaseRepository(AppDbContext context) => Context = context;

    // ── Template methods ─────────────────────────────────────────────────

    public virtual async Task<IEnumerable<T>> GetAllAsync() =>
        await DbSet.ToListAsync();

    public async Task<int> GetCountAsync() =>
        await DbSet.CountAsync();

    public async Task AddAsync(T entity)
    {
        await DbSet.AddAsync(entity);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        DbSet.Update(entity);
        await Context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await DbSet.FindAsync(id);
        if (entity is null) return;
        entity.IsDeleted = true;
        entity.DeletedAt = DateTime.UtcNow;
        await Context.SaveChangesAsync();
    }
}
