using FluentValidation;
using Tickets.Microservice.Entities;
using Tickets.Microservice.Validators;

namespace Tickets.Microservice.DependencyInjection;

internal static class ValidatorsDependencyInjection
{
    internal static void AddValidatorsDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IValidator<Ticket>, TicketValidator>();
    }
}
