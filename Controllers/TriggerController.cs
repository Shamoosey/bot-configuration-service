using DiscordBot_Backend.DTOs;
using DiscordBot_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiscordBot_Backend.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class TriggerController : ControllerBase
    {
        private readonly ILogger<TriggerController> _logger;
        private readonly ITriggerService _triggerService;

        public TriggerController(ILogger<TriggerController> logger, ITriggerService triggerService)
        {
            _logger = logger;
            _triggerService = triggerService;
        }

        [Route("GetAll")]
        [HttpGet]
        public async Task<List<TriggerDTO>> GetTriggers(Guid configId, CancellationToken cancellationToken)
        {
            return await _triggerService.GetTriggers(configId);
        }

        [Route("Get")]
        [HttpGet]
        public async Task<TriggerDTO> GetTrigger(Guid triggerId, CancellationToken cancellationToken)
        {
            return await _triggerService.GetTrigger(triggerId);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTrigger(Guid id, CancellationToken cancellationToken)
        {
            var result = await this._triggerService.DeleteTrigger(id);

            if(result)
            {
                return Ok();
            } 
            else
            {
                return NotFound();
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateTrigger(Guid triggerId, UpdateTriggerDTO TriggerDTO, CancellationToken cancellationToken)
        {
            var result = await this._triggerService.UpdateTrigger(triggerId, TriggerDTO);

            if (result)
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateTrigger(Guid configId, UpdateTriggerDTO trigger, CancellationToken cancellationToken)
        {
            var result = await this._triggerService.CreateTrigger(configId, trigger);

            if(result)
            {
                return Ok();
            } 
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}