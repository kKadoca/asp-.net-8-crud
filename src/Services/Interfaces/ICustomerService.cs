using BarbershopApi.Models;

namespace BarbershopApi.Services.Interfaces;

public interface ICustomerService
{
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<Customer?> GetByIdAsync(int id);
    Task<IEnumerable<Customer>> GetByNameAsync(string name);
    Task<int> GetCountAsync();
    Task CreateAsync(Customer customer);
    Task UpdateAsync(Customer customer);
    Task DeleteAsync(int id);
}
