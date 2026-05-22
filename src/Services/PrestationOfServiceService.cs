using BarbershopApi.Exceptions;
using BarbershopApi.Models;
using BarbershopApi.Repositories.Interfaces;
using BarbershopApi.Services.Interfaces;

namespace BarbershopApi.Services;

public class PrestationOfServiceService : IPrestationOfServiceService
{
    private readonly IPrestationOfServiceRepository _repository;

    public PrestationOfServiceService(IPrestationOfServiceRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<PrestationOfService>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<PrestationOfService?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<PrestationOfService>> GetByNameAsync(string name)
    {
        return await _repository.GetByNameAsync(name);
    }

    public async Task<int> GetCountAsync()
    {
        return await _repository.GetCountAsync();
    }

    public async Task CreateAsync(PrestationOfService prestationOfService)
    {
        if (prestationOfService.Price <= 0)
            throw new BusinessRuleException("Price must be greater than zero.");

        if (prestationOfService.DurationMinutes <= 0)
            throw new BusinessRuleException("Duration must be greater than zero minutes.");

        if (await _repository.ExistsByNameAsync(prestationOfService.Name))
            throw new BusinessRuleException($"A service named '{prestationOfService.Name}' already exists.");

        await _repository.AddAsync(prestationOfService);
    }

    public async Task UpdateAsync(PrestationOfService prestationOfService)
    {
        _ = await _repository.GetByIdAsync(prestationOfService.Id)
            ?? throw new NotFoundException(nameof(PrestationOfService), prestationOfService.Id);

        if (prestationOfService.Price <= 0)
            throw new BusinessRuleException("Price must be greater than zero.");

        if (prestationOfService.DurationMinutes <= 0)
            throw new BusinessRuleException("Duration must be greater than zero minutes.");

        if (await _repository.ExistsByNameAsync(prestationOfService.Name, excludeId: prestationOfService.Id))
            throw new BusinessRuleException($"A service named '{prestationOfService.Name}' already exists.");

        await _repository.UpdateAsync(prestationOfService);
    }

    public async Task DeleteAsync(int id)
    {
        _ = await _repository.GetByIdAsync(id)
            ?? throw new NotFoundException(nameof(PrestationOfService), id);

        await _repository.DeleteAsync(id);
    }
}
