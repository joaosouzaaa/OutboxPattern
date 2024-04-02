using FluentValidation;
using Support.Microservice.DataTransferObjects.SupportEngineer;
using Support.Microservice.Entities;
using Support.Microservice.Enums;
using Support.Microservice.Extensions;
using Support.Microservice.Interfaces.Mappers;
using Support.Microservice.Interfaces.Repositories;
using Support.Microservice.Interfaces.Services;
using Support.Microservice.Interfaces.Settings;

namespace Support.Microservice.Services;

public sealed class SupportEngineerService(ISupportEngineerRepository supportEngineerRepository,
                                           ISupportEngineerMapper supportEngineerMapper,
                                           IValidator<SupportEngineer> validator,
                                           INotificationHandler notificationHandler)
                                           : ISupportEngineerService
{
    public async Task<bool> AddAsync(SupportEngineerSave supportEngineerSave)
    {
        var supportEngineer = supportEngineerMapper.SaveToDomain(supportEngineerSave);

        if (!await ValidateAsync(supportEngineer))
            return false;

        return await supportEngineerRepository.AddAsync(supportEngineer);
    }

    public async Task<bool> UpdateAsync(SupportEngineerUpdate supportEngineerUpdate)
    {
        var supportEngineer = await supportEngineerRepository.GetByIdAsync(supportEngineerUpdate.Id);

        if (supportEngineer is null)
        {
            notificationHandler.AddNotification(nameof(EMessage.NotFound), EMessage.NotFound.Description().FormatTo("Support Engineer"));

            return false;
        }

        supportEngineerMapper.UpdateToDomain(supportEngineerUpdate, supportEngineer);

        if (!await ValidateAsync(supportEngineer))
            return false;

        return await supportEngineerRepository.UpdateAsync(supportEngineer);
    }

    public async Task<List<SupportEngineerResponse>> GetAllAsync()
    {
        var supportEngineerList = await supportEngineerRepository.GetAllAsync();

        return supportEngineerMapper.DomainListToDomainResponse(supportEngineerList);
    }

    private async Task<bool> ValidateAsync(SupportEngineer supportEngineer)
    {
        var validationResult = await validator.ValidateAsync(supportEngineer);

        if (validationResult.IsValid)
            return true;

        foreach (var error in validationResult.Errors)
        {
            notificationHandler.AddNotification(error.PropertyName, error.ErrorMessage);
        }

        return false;
    }
}
