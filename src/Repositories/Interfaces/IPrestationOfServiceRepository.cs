using BarbershopApi.Models;

namespace BarbershopApi.Repositories.Interfaces;

public interface IPrestationOfServiceRepository
{
    Task<IEnumerable<PrestationOfService>> GetAllAsync();
    Task<PrestationOfService?> GetByIdAsync(int id);
    Task<IEnumerable<PrestationOfService>> GetByNameAsync(string name);
    Task<int> GetCountAsync();
    Task AddAsync(PrestationOfService prestationOfService);
    Task UpdateAsync(PrestationOfService prestationOfService);
    Task DeleteAsync(int id);
}
