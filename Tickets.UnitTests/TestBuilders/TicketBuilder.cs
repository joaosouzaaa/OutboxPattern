using Tickets.Microservice.DataTransferObjects.Ticket;
using Tickets.Microservice.Entities;

namespace Tickets.UnitTests.TestBuilders;
public sealed class TicketBuilder
{
    private readonly Guid _id = Guid.NewGuid();
    private string _title = "test";
    private int _number = 123;
    private string _tag = "rand";
    private string _description = "random with more than 20 characters";
    private readonly DateTime _createdDate = DateTime.Now;
    private readonly DateTime _firstAppearance = DateTime.Now;

    public static TicketBuilder NewObject() =>
        new();

    public Ticket DomainBuild() =>
        new()
        {
            FirstAppearance = _firstAppearance,
            CreatedDate = _createdDate,
            Description = _description,
            Id = _id,
            Number = _number,
            Tag = _tag,
            Title = _title
        };

    public TicketSave SaveBuild() =>
        new(_title,
            _number,
            _tag,
            _description,
            _firstAppearance);

    public TicketUpdate UpdateBuild() =>
        new(_id,
            _title,
            _number,
            _tag,
            _description,
            _firstAppearance);

    public TicketResponse ResponseBuild() =>
        new()
        {
            FirstAppearance = _firstAppearance,
            CreatedDate = _createdDate,
            Description = _description,
            Id = _id,
            Number = _number,
            Tag = _tag,
            Title = _title
        };

    public TicketBuilder WithTitle(string title)
    {
        _title = title;

        return this;
    }

    public TicketBuilder WithNumber(int number)
    {
        _number = number;

        return this;
    }

    public TicketBuilder WithTag(string tag)
    {
        _tag = tag;

        return this;
    }

    public TicketBuilder WithDescription(string description)
    {
        _description = description;

        return this;
    }
}
