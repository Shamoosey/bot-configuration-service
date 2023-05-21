using Joebot_Backend.DTOs;
using Joebot_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Joebot_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly ILogger<StatusController> _logger;
        private readonly IStatusService _statusService;

        public StatusController(ILogger<StatusController> logger, IStatusService statusService)
        {
            _logger = logger;
            _statusService = statusService;
        }

        [HttpGet]
        public async Task<List<StatusMessageDTO>> GetConfiguration(CancellationToken cancellationToken)
        {
            return await _statusService.GetStatusMessages();
        }

        [HttpDelete]
        public async Task<bool> DeleteStatus(Guid statusId, CancellationToken cancellationToken)
        {
            return await this._statusService.DeleteStatusMessage(statusId);
        }

        [HttpPost]
        public async Task<bool> CreateStatusMessage(StatusMessageDTO status, CancellationToken cancellationToken)
        {
            return await this._statusService.CreateStatusMessage(status);
        }
    }
}