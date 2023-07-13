using AutoMapper;
using Joebot_Backend.Database;
using Joebot_Backend.Database.Models;
using Joebot_Backend.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Joebot_Backend.Services
{
    public class UserService : IUserService
    {
        private readonly JoeContext _joeContext;
        private readonly IMapper _mapper;

        public UserService(JoeContext joeContext, IMapper mapper)
        {
            _joeContext = joeContext;
            _mapper = mapper;
        }

        public async Task<bool> CreateUser(UserDTO userDto, string serverId)
        {
            bool result = false;
            try
            {
                var existingConfiguration = await _joeContext.Configurations.FirstOrDefaultAsync(x => x.ServerId == serverId);

                if(existingConfiguration == null) 
                {
                    throw new Exception($"No configuration exists with serverId {serverId}");
                }

                var userEntity = _mapper.Map<User>(userDto);
                userEntity.Configuration = existingConfiguration;
                _joeContext.Users.Add(userEntity);
                await _joeContext.SaveChangesAsync();

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


        public async Task<bool> UpdateUser(Guid userId, UserDTO userDto)
        {
            bool result = false;
            try
            {
                var userEntity = await _joeContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

                if (userEntity == null)
                {
                    throw new Exception("User does not exist");
                }


                _mapper.Map(userDto, userEntity);
                await _joeContext.SaveChangesAsync();

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
                var userEntity = await _joeContext.Users.FirstOrDefaultAsync(x => x.Id == id);

                if (userEntity == null)
                {
                    throw new Exception("User does not exist");
                }

                _joeContext.Users.Remove(userEntity);
                await _joeContext.SaveChangesAsync();

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
                var userEntity = await _joeContext.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();

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

        public async Task<List<UserDTO>> GetUsers(string serverId)
        {
            try
            {
                var userEntities = await _joeContext.Users.Where(x => x.Configuration.ServerId == serverId).ToListAsync();
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
        public Task<bool> CreateUser(UserDTO User, string serverId);
        public Task<bool> UpdateUser(Guid userId, UserDTO User);
        public Task<bool> DeleteUser(Guid userId);
        public Task<UserDTO> GetUser(Guid userId);
        public Task<List<UserDTO>> GetUsers(string serverId);
    }
}