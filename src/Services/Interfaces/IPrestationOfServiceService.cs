using BarbershopApi.Models;

namespace BarbershopApi.Services.Interfaces;

public interface IPrestationOfServiceService
{
    Task<IEnumerable<PrestationOfService>> GetAllAsync();
    Task<PrestationOfService?> GetByIdAsync(int id);
    Task<IEnumerable<PrestationOfService>> GetByNameAsync(string name);
    Task<int> GetCountAsync();
    Task CreateAsync(PrestationOfService prestationOfService);
    Task UpdateAsync(PrestationOfService prestationOfService);
    Task DeleteAsync(int id);
}
