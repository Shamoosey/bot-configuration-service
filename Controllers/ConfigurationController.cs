using DiscordBot_Backend.DTOs;
using DiscordBot_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace DiscordBot_Backend.Controllers
{
    [ApiController]
    [Authorize("read:configurations")]
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
        public async Task<ActionResult> GetConfiguration(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await this._configurationService.GetConfiguration(id));
        }

        [Route("GetAll")]
        [HttpGet]
        public async Task<ActionResult> GetConfigurations(CancellationToken cancellationToken)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var guilds = claims.Where(x => x.Type == "user_guilds").Select(x => x.Value).ToList();
                var showAllEnabledString = claims.Where(x => x.Type == "show_all_guilds").Select(x => x.Value).FirstOrDefault() ?? "false";
                bool showAllEnabled = Convert.ToBoolean(showAllEnabledString);
                return Ok(await this._configurationService.GetConfigurations(guilds, showAllEnabled));
            }
            
            return StatusCode(StatusCodes.Status418ImATeapot);
        }

        [HttpPost]
        [Authorize("edit:configurations")]
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
        [Authorize("edit:configurations")]
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
        [Authorize("delete:configurations")]
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