using Microsoft.AspNetCore.Mvc;
using Support.Microservice.DataTransferObjects.SupportEngineer;
using Support.Microservice.Interfaces.Services;
using Support.Microservice.Settings.NotificationSettings;

namespace Support.Microservice.Controllers;
[Route("api/[controller]")]
[ApiController]
public sealed class SupportEngineerController(ISupportEngineerService supportEngineerService) : ControllerBase
{
    [HttpPost("add")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<Notification>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<bool> AddAsync([FromBody] SupportEngineerSave supportEngineerSave) =>
        supportEngineerService.AddAsync(supportEngineerSave);

    [HttpPut("update")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<Notification>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<bool> UpdateAsync([FromBody] SupportEngineerUpdate supportEngineerUpdate) =>
        supportEngineerService.UpdateAsync(supportEngineerUpdate);

    [HttpGet("get-all")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SupportEngineerResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<List<SupportEngineerResponse>> GetAllAsync() =>
        supportEngineerService.GetAllAsync();
}
