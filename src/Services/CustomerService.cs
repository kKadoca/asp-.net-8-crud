using BarbershopApi.Exceptions;
using BarbershopApi.Models;
using BarbershopApi.Repositories.Interfaces;
using BarbershopApi.Services.Interfaces;

namespace BarbershopApi.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;

    public CustomerService(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Customer?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Customer>> GetByNameAsync(string name)
    {
        return await _repository.GetByNameAsync(name);
    }

    public async Task<int> GetCountAsync()
    {
        return await _repository.GetCountAsync();
    }

    public async Task CreateAsync(Customer customer)
    {
        if (!customer.Email.Contains('@') || !customer.Email.Contains(".com"))
            throw new BusinessRuleException("Email address is invalid.");

        if (await _repository.ExistsByEmailAsync(customer.Email))
            throw new BusinessRuleException($"Email '{customer.Email}' is already in use.");

        await _repository.AddAsync(customer);
    }

    public async Task UpdateAsync(Customer customer)
    {
        _ = await _repository.GetByIdAsync(customer.Id)
            ?? throw new NotFoundException(nameof(Customer), customer.Id);

        if (!customer.Email.Contains('@') || !customer.Email.Contains(".com"))
            throw new BusinessRuleException("Email address is invalid.");

        if (await _repository.ExistsByEmailAsync(customer.Email, excludeId: customer.Id))
            throw new BusinessRuleException($"Email '{customer.Email}' is already in use.");

        await _repository.UpdateAsync(customer);
    }

    public async Task DeleteAsync(int id)
    {
        _ = await _repository.GetByIdAsync(id)
            ?? throw new NotFoundException(nameof(Customer), id);

        await _repository.DeleteAsync(id);
    }
}
