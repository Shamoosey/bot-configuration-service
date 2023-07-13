using Joebot_Backend.Database.Models;
using Joebot_Backend.DTOs;
using Joebot_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Joebot_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [Route("GetAll")]
        [HttpGet]
        public async Task<List<UserDTO>> GetAll(string serverId, CancellationToken cancellationToken)
        {
            return await _userService.GetUsers(serverId);
        }

        [Route("Get")]
        [HttpGet]
        public async Task<UserDTO> Get(Guid userId, CancellationToken cancellationToken)
        {
            return await _userService.GetUser(userId);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(Guid id, CancellationToken cancellationToken)
        {
            var result = await this._userService.DeleteUser(id);

            if(result)
            {
                return Ok();
            } 
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(UserDTO user, string serverId, CancellationToken cancellationToken)
        {
            var result = await this._userService.CreateUser(user, serverId);

            if(result)
            {
                return Ok();
            } 
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(Guid userId, UserDTO userDto, CancellationToken cancellationToken)
        {
            var result = await this._userService.UpdateUser(userId, userDto);

            if (result)
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