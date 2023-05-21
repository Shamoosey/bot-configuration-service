using AutoMapper;
using Joebot_Backend.Controllers;
using Joebot_Backend.Database;
using Joebot_Backend.Database.Models;
using Joebot_Backend.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Joebot_Backend.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly ILogger<ConfigurationController> _logger;
        private readonly JoeContext _joeContext;
        private readonly IMapper _mapper;

        public ConfigurationService(JoeContext joeContext, IMapper mapper, ILogger<ConfigurationController> logger)
        {
            _joeContext = joeContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> CreateConfiguration(ConfigurationDTO configuration)
        {
            bool result = false;
            try
            {
                var existingConfiguration = await _joeContext.Configurations.FirstOrDefaultAsync(x => x.ServerId == configuration.ServerId);
                if (existingConfiguration != null)
                {
                    throw new Exception("Configuration already exists");
                }

                var newConfiguration = _mapper.Map<Configuration>(configuration);
                newConfiguration.Id = Guid.NewGuid();
                _joeContext.Configurations.Add(newConfiguration);
                await _joeContext.SaveChangesAsync();
                result = true;
            } catch (Exception e)
            {
                // todo - add logging
                Console.WriteLine(e);
                result = false;
            }
            return result;
        }

        public async Task<bool> UpdateConfiguration(string serverId, EditConfigurationDTO configuration)
        {
            bool result = false;
            try
            {
                var existingConfiguration = await _joeContext.Configurations.FirstOrDefaultAsync(x => x.ServerId == serverId);
                if (existingConfiguration == null)
                {
                    throw new Exception("Configuration doesn't exist");
                }

                _mapper.Map(configuration, existingConfiguration);
                await _joeContext.SaveChangesAsync();
                result = true;
            } catch (Exception e)
            {
                // todo - add logging
                Console.WriteLine(e);
                result = false;
            }
            return result;
        }

        public async Task<JoeConfigDTO> GetConfiguration(string serverId)
        {
            var configuration = await _joeContext.Configurations
                .Include(c => c.Users)
                .Include(c => c.Triggers).ThenInclude(x => x.TriggerResponses)
                .Include(c => c.Triggers).ThenInclude(x => x.TriggerWords)
                .Include(c => c.Triggers).ThenInclude(x => x.ReactEmotes)
                .FirstOrDefaultAsync(c => c.ServerId == serverId);

            return _mapper.Map<JoeConfigDTO>(configuration);
        }

        public async Task<bool> DeleteConfiguration(string serverId)
        {
            bool result = false;
            try
            {
                var configuration = await _joeContext.Configurations.FirstOrDefaultAsync(x => x.ServerId == serverId);
                if (configuration == null)
                {
                    throw new Exception("Configuration doesn't exist");
                }   

                _joeContext.Configurations.Remove(configuration);
                await _joeContext.SaveChangesAsync();
                result = true;
            }             
            catch (Exception e)
            {
                // todo - add logging
                Console.WriteLine(e);
                result = false;
            }
            return result;
        }
    }

    public interface IConfigurationService
    {
        Task<JoeConfigDTO> GetConfiguration(string serverId);
        Task<bool> UpdateConfiguration(string serverId, EditConfigurationDTO configuration);
        Task<bool> CreateConfiguration(ConfigurationDTO configuration);
        Task<bool> DeleteConfiguration(string serverId);
    }
}
