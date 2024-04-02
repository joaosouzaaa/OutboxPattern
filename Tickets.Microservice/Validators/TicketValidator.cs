using FluentValidation;
using Tickets.Microservice.Entities;
using Tickets.Microservice.Enums;
using Tickets.Microservice.Extensions;

namespace Tickets.Microservice.Validators;

public sealed class TicketValidator : AbstractValidator<Ticket>
{
    public TicketValidator()
    {
        RuleFor(t => t.Title).Length(3, 150)
            .WithMessage(EMessage.InvalidLength.Description().FormatTo("Title", "3 to 150"));

        RuleFor(t => t.Number).GreaterThan(0)
            .WithMessage(EMessage.GreaterThan.Description().FormatTo("Number", "0"));

        RuleFor(t => t.Tag).Length(1, 150)
            .WithMessage(EMessage.InvalidLength.Description().FormatTo("Tag", "1 to 150"));

        RuleFor(t => t.Description).Length(20, 2000)
            .WithMessage(EMessage.InvalidLength.Description().FormatTo("Description", "20 to 2000"));
    }
}
