using BarbershopApi.Exceptions;
using BarbershopApi.Models;
using BarbershopApi.Repositories.Interfaces;
using BarbershopApi.Services.Interfaces;

namespace BarbershopApi.Services;

public class ProfessionalService : IProfessionalService
{
    private readonly IProfessionalRepository _repository;

    public ProfessionalService(IProfessionalRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Professional>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Professional?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Professional>> GetByNameAsync(string name)
    {
        return await _repository.GetByNameAsync(name);
    }

    public async Task<int> GetCountAsync()
    {
        return await _repository.GetCountAsync();
    }

    public async Task CreateAsync(Professional professional)
    {
        if (await _repository.ExistsByPhoneAsync(professional.Phone))
            throw new BusinessRuleException($"Phone '{professional.Phone}' is already registered to another professional.");

        await _repository.AddAsync(professional);
    }

    public async Task UpdateAsync(Professional professional)
    {
        _ = await _repository.GetByIdAsync(professional.Id)
            ?? throw new NotFoundException(nameof(Professional), professional.Id);

        if (await _repository.ExistsByPhoneAsync(professional.Phone, excludeId: professional.Id))
            throw new BusinessRuleException($"Phone '{professional.Phone}' is already registered to another professional.");

        await _repository.UpdateAsync(professional);
    }

    public async Task DeleteAsync(int id)
    {
        _ = await _repository.GetByIdAsync(id)
            ?? throw new NotFoundException(nameof(Professional), id);

        await _repository.DeleteAsync(id);
    }
}
