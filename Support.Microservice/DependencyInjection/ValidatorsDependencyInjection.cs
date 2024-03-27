using FluentValidation;
using Support.Microservice.Entities;
using Support.Microservice.Validators;

namespace Support.Microservice.DependencyInjection;

internal static class ValidatorsDependencyInjection
{
    internal static void AddValidatorsDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IValidator<SupportEngineer>, SupportEngineerValidator>();
    }
}
