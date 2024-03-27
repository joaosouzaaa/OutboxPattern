using FluentValidation;
using Support.Microservice.Entities;
using Support.Microservice.Enums;
using Support.Microservice.Extensions;

namespace Support.Microservice.Validators;

public sealed class SupportEngineerValidator : AbstractValidator<SupportEngineer>
{
    public SupportEngineerValidator()
    {
        RuleFor(s => s.Name).Length(2, 200)
            .WithMessage(EMessage.InvalidLength.Description().FormatTo("Name"));

        RuleFor(s => s.Email).EmailAddress()
            .WithMessage(EMessage.InvalidFormat.Description().FormatTo("Email"));
    }
}
