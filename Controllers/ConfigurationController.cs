using Joebot_Backend.DTOs;
using Joebot_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Joebot_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigurationController : ControllerBase
    {
        private readonly ILogger<ConfigurationController> _logger;
        private readonly IConfigurationService _configurationService;

        public ConfigurationController(ILogger<ConfigurationController> logger, IConfigurationService configurationService)
        {
            _logger = logger;
            _configurationService = configurationService;
        }

        [HttpGet]
        public async Task<ConfigurationDTO?> GetConfiguration(string serverId, CancellationToken cancellationToken)
        {
            return await this._configurationService.GetConfiguration(serverId);
        }

        [HttpPost]
        public async Task<bool> CreateConfiguration(ConfigurationDTO configuration, CancellationToken cancellationToken)
        {
            return await this._configurationService.CreateConfiguration(configuration);
        }

        [HttpPut]
        public async Task<bool> UpdateConfiguration(ConfigurationDTO configuration, CancellationToken cancellationToken)
        {
            return await this._configurationService.UpdateConfiguration(configuration);
        }
    }
}