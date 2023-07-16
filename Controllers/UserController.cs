using DiscordBot_Backend.Database.Models;
using DiscordBot_Backend.DTOs;
using DiscordBot_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiscordBot_Backend.Controllers
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
        public async Task<List<UserDTO>> GetAll(Guid configId, CancellationToken cancellationToken)
        {
            return await _userService.GetUsers(configId);
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
        public async Task<ActionResult> CreateUser(UpdateUserDTO user, Guid configId, CancellationToken cancellationToken)
        {
            var result = await this._userService.CreateUser(user, configId);

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
        public async Task<ActionResult> UpdateUser(Guid userId, UpdateUserDTO userDto, CancellationToken cancellationToken)
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