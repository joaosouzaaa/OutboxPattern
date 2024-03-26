using Tickets.Microservice.Settings.PaginationSettings;

namespace Tickets.Microservice.Arguments;

public sealed class GetAllTicketsFilteredArgument : PageParameters
{
    public string? SearchText { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
