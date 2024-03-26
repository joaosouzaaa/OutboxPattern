using Tickets.Microservice.Entities;
using Tickets.Microservice.Mappers;
using Tickets.Microservice.Settings.PaginationSettings;
using Tickets.UnitTests.TestBuilders;

namespace Tickets.UnitTests.MappersTests;
public sealed class TicketMapperTests
{
    private readonly TicketMapper _ticketMapper;

    public TicketMapperTests()
    {
        _ticketMapper = new TicketMapper();
    }

    [Fact]
    public void SaveToDomain_SuccessfulScenario_ReturnsDomainObject()
    {
        // A
        var ticketSave = TicketBuilder.NewObject().SaveBuild();

        // A
        var ticketResult = _ticketMapper.SaveToDomain(ticketSave);

        // A
        Assert.Equal(ticketResult.FirstAppearance, ticketSave.FirstAppearance);
        Assert.Equal(ticketResult.Description, ticketSave.Description);
        Assert.Equal(ticketResult.Number, ticketSave.Number);
        Assert.Equal(ticketResult.Tag, ticketSave.Tag);
        Assert.Equal(ticketResult.Title, ticketSave.Title);
    }

    [Fact]
    public void UpdateToDomain_SuccessfulScenario_MapsProperly()
    {
        // A
        var ticketUpdate = TicketBuilder.NewObject().UpdateBuild();
        var ticketResult = TicketBuilder.NewObject().DomainBuild();

        // A
        _ticketMapper.UpdateToDomain(ticketUpdate, ticketResult);

        // A
        Assert.Equal(ticketResult.FirstAppearance, ticketUpdate.FirstAppearance);
        Assert.Equal(ticketResult.Description, ticketUpdate.Description);
        Assert.Equal(ticketResult.Number, ticketUpdate.Number);
        Assert.Equal(ticketResult.Tag, ticketUpdate.Tag);
        Assert.Equal(ticketResult.Title, ticketUpdate.Title);
    }

    [Fact]
    public void DomainToResponse_SuccessfulScenario_ReturnsResponseObject()
    {
        // A
        var ticket = TicketBuilder.NewObject().DomainBuild();

        // A
        var ticketResponseResult = _ticketMapper.DomainToResponse(ticket);

        // A
        Assert.Equal(ticketResponseResult.FirstAppearance, ticket.FirstAppearance);
        Assert.Equal(ticketResponseResult.CreatedDate, ticket.CreatedDate);
        Assert.Equal(ticketResponseResult.Description, ticket.Description);
        Assert.Equal(ticketResponseResult.Id, ticket.Id);
        Assert.Equal(ticketResponseResult.Number, ticket.Number);
        Assert.Equal(ticketResponseResult.Tag, ticket.Tag);
        Assert.Equal(ticketResponseResult.Title, ticket.Title);
    }

    [Fact]
    public void DomainPageListToResponsePageList_SuccessfulScenario_ReturnsResponsePageList()
    {
        // A
        var ticketList = new List<Ticket>()
        {
            TicketBuilder.NewObject().DomainBuild(),
            TicketBuilder.NewObject().DomainBuild(),
            TicketBuilder.NewObject().DomainBuild()
        };
        var ticketPageList = new PageList<Ticket>()
        {
            CurrentPage = 1,
            PageSize = 2,
            Result = ticketList,
            TotalCount = 8,
            TotalPages = 9
        };

        // A
        var ticketResponsePageListResult = _ticketMapper.DomainPageListToResponsePageList(ticketPageList);

        // A
        Assert.Equal(ticketResponsePageListResult.CurrentPage, ticketPageList.CurrentPage);
        Assert.Equal(ticketResponsePageListResult.PageSize, ticketPageList.PageSize);
        Assert.Equal(ticketResponsePageListResult.Result.Count, ticketPageList.Result.Count);
        Assert.Equal(ticketResponsePageListResult.TotalCount, ticketPageList.TotalCount);
        Assert.Equal(ticketResponsePageListResult.TotalPages, ticketPageList.TotalPages);
    }
}
