using Microsoft.AspNetCore.Mvc;
using Tickets.Microservice.Arguments;
using Tickets.Microservice.DataTransferObjects.Ticket;
using Tickets.Microservice.Interfaces.Services;
using Tickets.Microservice.Settings.NotificationSettings;
using Tickets.Microservice.Settings.PaginationSettings;

namespace Tickets.Microservice.Controllers;
[Route("api/[controller]")]
[ApiController]
public sealed class TicketController(ITicketService ticketService) : ControllerBase
{
    [HttpPost("add")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<Notification>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<bool> AddAsync([FromBody] TicketSave ticketSave) =>
        ticketService.AddAsync(ticketSave);

    [HttpPut("update")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<Notification>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<bool> UpdateAsync([FromBody] TicketUpdate ticketUpdate) =>
        ticketService.UpdateAsync(ticketUpdate);

    [HttpDelete("delete")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<Notification>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<bool> DeleteAsync([FromQuery] Guid id) =>
        ticketService.DeleteAsync(id);

    [HttpGet("get-by-id")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TicketResponse))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<TicketResponse?> GetByIdAsync([FromQuery] Guid id) =>
        ticketService.GetByIdAsync(id);

    [HttpGet("get-all-paginated")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PageList<TicketResponse>))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<PageList<TicketResponse>> GetAllPaginatedAsync([FromQuery] GetAllTicketsFilteredArgument filter) =>
        ticketService.GetAllPaginatedAsync(filter);
}
