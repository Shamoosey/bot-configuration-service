using AutoMapper;
using DiscordBot_Backend.Database;
using DiscordBot_Backend.Database.Models;
using DiscordBot_Backend.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DiscordBot_Backend.Services
{
    public class UserService : IUserService
    {
        private readonly BotContext _botContext;
        private readonly IMapper _mapper;

        public UserService(BotContext botContext, IMapper mapper)
        {
            _botContext = botContext;
            _mapper = mapper;
        }

        public async Task<bool> CreateUser(UpdateUserDTO userDto, Guid configId)
        {
            bool result = false;
            try
            {
                var existingConfiguration = await _botContext.Configurations.FirstOrDefaultAsync(x => x.Id == configId);

                if(existingConfiguration == null) 
                {
                    throw new Exception($"No configuration exists with id {configId}");
                }

                var userEntity = _mapper.Map<User>(userDto);
                userEntity.Configuration = existingConfiguration;
                _botContext.Users.Add(userEntity);
                await _botContext.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                // Handle exception or log error
                Console.WriteLine(ex);
                result = false;
            }

            return result;
        }


        public async Task<bool> UpdateUser(Guid userId, UpdateUserDTO userDto)
        {
            bool result = false;
            try
            {
                var userEntity = await _botContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

                if (userEntity == null)
                {
                    throw new Exception("User does not exist");
                }

                userEntity.UserName= userDto.UserName;
                userEntity.DiscordUserId = userDto.DiscordUserId;
                userEntity.IsSecert = userDto.IsSecert;
                await _botContext.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                // Handle exception or log error
                Console.WriteLine(ex);
                result = false;
            }

            return result;
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            bool result = false;
            try
            {
                var userEntity = await _botContext.Users.FirstOrDefaultAsync(x => x.Id == id);

                if (userEntity == null)
                {
                    throw new Exception("User does not exist");
                }

                _botContext.Users.Remove(userEntity);
                await _botContext.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                // Handle exception or log error
                Console.WriteLine(ex);
                result = false;
            }

            return result;
        }

        public async Task<UserDTO> GetUser(Guid userId)
        {
            UserDTO? userDto = null;
            try
            {
                var userEntity = await _botContext.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();

                if(userEntity == null)
                {
                    throw new Exception("User does not exist");
                }

                userDto = _mapper.Map<UserDTO>(userEntity);

            }
            catch (Exception ex)
            {
                throw new Exception("Could not get user", ex);
            }

            return userDto;
        }

        public async Task<List<UserDTO>> GetUsers(Guid configId)
        {
            try
            {
                var userEntities = await _botContext.Users.Where(x => x.Configuration.Id == configId).ToListAsync();
                var userDtos = _mapper.Map<List<User>, List<UserDTO>>(userEntities);

                return userDtos;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not get user", ex);
            }
        }
    }

    public interface IUserService
    {
        public Task<bool> CreateUser(UpdateUserDTO User, Guid configId);
        public Task<bool> UpdateUser(Guid userId, UpdateUserDTO User);
        public Task<bool> DeleteUser(Guid userId);
        public Task<UserDTO> GetUser(Guid userId);
        public Task<List<UserDTO>> GetUsers(Guid configId);
    }
}