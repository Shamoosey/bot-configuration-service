using DiscordBot_Backend.DTOs;
using DiscordBot_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiscordBot_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigurationController : ControllerBase
    {
        private readonly ILogger<ConfigurationController> _logger;
        private readonly IConfigurationService _configurationService;

        public ConfigurationController(
            ILogger<ConfigurationController> logger, 
            IConfigurationService configurationService
        )
        {
            _logger = logger;
            _configurationService = configurationService;
        }

        [Route("Get")]
        [HttpGet]
        public async Task<ConfigurationDTO?> GetConfiguration(Guid id, CancellationToken cancellationToken)
        {
            return await this._configurationService.GetConfiguration(id);
        }

        [Route("GetAll")]
        [HttpGet]
        public async Task<IEnumerable<ConfigurationDTO>> GetConfigurations(CancellationToken cancellationToken)
        {
            return await this._configurationService.GetAllConfigurations();
        }

        [HttpPost]
        public async Task<ActionResult> CreateConfiguration(UpdateConfigurationDTO configuration, CancellationToken cancellationToken)
        {
            var result = await this._configurationService.CreateConfiguration(configuration);

            if (result)
            {
                return Ok();
            } 
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateConfiguration(Guid id, UpdateConfigurationDTO configuration, CancellationToken cancellationToken)
        {
            var result = await this._configurationService.UpdateConfiguration(id, configuration);

            if(result)
            {
                return Ok(result);
            } 
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteConfiguration(Guid id, CancellationToken cancellationToken)
        {
            var result = await _configurationService.DeleteConfiguration(id);

            if (result)
            {
                return Ok(result);
            } 
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
    }
}