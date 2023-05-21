using Joebot_Backend.DTOs;
using Joebot_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Joebot_Backend.Controllers
{
    [ApiController]
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
        public async Task<List<TriggerDTO>> GetTriggers(string serverId, CancellationToken cancellationToken)
        {
            return await _triggerService.GetTriggers(serverId);
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
        public async Task<ActionResult> UpdateTrigger(Guid triggerId, EditTriggerDTO editTriggerDto, CancellationToken cancellationToken)
        {
            var result = await this._triggerService.UpdateTrigger(triggerId, editTriggerDto);

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
        public async Task<ActionResult> CreateTrigger(string serverId, EditTriggerDTO trigger, CancellationToken cancellationToken)
        {
            var result = await this._triggerService.CreateTrigger(serverId, trigger);

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