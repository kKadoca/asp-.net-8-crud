using BarbershopApi.Data;
using BarbershopApi.Models;
using BarbershopApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BarbershopApi.Repositories;

public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(AppDbContext context) : base(context) { }

    protected override DbSet<Customer> DbSet => Context.Customers;

    public async Task<Customer?> GetByIdAsync(int id) =>
        await DbSet.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

    public async Task<IEnumerable<Customer>> GetByNameAsync(string name) =>
        await DbSet.Where(c => c.Name.Contains(name)).ToListAsync();

    public async Task<bool> ExistsByEmailAsync(string email, int? excludeId = null) =>
        await DbSet.AnyAsync(c => c.Email == email && (excludeId == null || c.Id != excludeId));
}
