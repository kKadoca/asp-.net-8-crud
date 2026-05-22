using BarbershopApi.Models;

namespace BarbershopApi.Services.Interfaces;

public interface IProfessionalService
{
    Task<IEnumerable<Professional>> GetAllAsync();
    Task<Professional?> GetByIdAsync(int id);
    Task<IEnumerable<Professional>> GetByNameAsync(string name);
    Task<int> GetCountAsync();
    Task CreateAsync(Professional professional);
    Task UpdateAsync(Professional professional);
    Task DeleteAsync(int id);
}
