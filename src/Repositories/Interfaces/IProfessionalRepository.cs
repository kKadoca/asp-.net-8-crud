using BarbershopApi.Models;

namespace BarbershopApi.Repositories.Interfaces;

public interface IProfessionalRepository
{
    Task<IEnumerable<Professional>> GetAllAsync();
    Task<Professional?> GetByIdAsync(int id);
    Task<IEnumerable<Professional>> GetByNameAsync(string name);
    Task<int> GetCountAsync();
    Task AddAsync(Professional professional);
    Task UpdateAsync(Professional professional);
    Task DeleteAsync(int id);
}
