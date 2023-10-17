using AutoMapper;
using DiscordBot_Backend.Controllers;
using DiscordBot_Backend.Database;
using DiscordBot_Backend.Database.Models;
using DiscordBot_Backend.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DiscordBot_Backend.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly ILogger<ConfigurationService> _logger;
        private readonly BotContext _botContext;
        private readonly IMapper _mapper;

        public ConfigurationService(BotContext botContext, IMapper mapper, ILogger<ConfigurationService> logger)
        {
            _botContext = botContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> CreateConfiguration(UpdateConfigurationDTO configuration)
        {
            bool result = false;
            try
            {
                var newConfiguration = _mapper.Map<Configuration>(configuration);
                newConfiguration.Id = Guid.NewGuid();
                _botContext.Configurations.Add(newConfiguration);
                await _botContext.SaveChangesAsync();
                result = true;
            } catch (Exception e)
            {
                // todo - add logging
                Console.WriteLine(e);
                result = false;
            }
            return result;
        }

        public async Task<bool> UpdateConfiguration(Guid configId, UpdateConfigurationDTO configuration)
        {
            bool result = false;
            try
            {
                var existingConfiguration = await _botContext.Configurations
                    .FirstOrDefaultAsync(x => x.Id == configId);

                if (existingConfiguration == null)
                {
                    throw new Exception("Configuration doesn't exist");
                }

                _mapper.Map(configuration, existingConfiguration);

                await _botContext.SaveChangesAsync();
                result = true;
            } catch (Exception e)
            {
                // todo - add logging
                Console.WriteLine(e);
                result = false;
            }
            return result;
        }

        public async Task<ConfigurationDTO> GetConfiguration(Guid configId)
        {
            var configuration = await _botContext.Configurations
                .Include(c => c.Users)
                .Include(c => c.Triggers).ThenInclude(x => x.TriggerResponses)
                .Include(c => c.Triggers).ThenInclude(x => x.TriggerWords)
                .Include(c => c.Triggers).ThenInclude(x => x.ReactEmotes)
                .FirstOrDefaultAsync(c => c.Id == configId);

            return _mapper.Map<ConfigurationDTO>(configuration);
        }

        public async Task<IEnumerable<ConfigurationDTO>> GetConfigurations(List<string> configs)
        {
            var configuration = await _botContext.Configurations
                .Include(c => c.Users)
                .Include(c => c.Triggers).ThenInclude(x => x.TriggerResponses)
                .Include(c => c.Triggers).ThenInclude(x => x.TriggerWords)
                .Include(c => c.Triggers).ThenInclude(x => x.ReactEmotes)
                .Where(c => configs.Contains(c.ServerId.Trim()))
                .ToListAsync();

            return _mapper.Map<IEnumerable<ConfigurationDTO>>(configuration);
        }

        public async Task<bool> DeleteConfiguration(Guid configId)
        {
            bool result = false;
            try
            {
                var configuration = await _botContext.Configurations.FirstOrDefaultAsync(x => x.Id == configId);
                if (configuration == null)
                {
                    throw new Exception("Configuration doesn't exist");
                }   

                _botContext.Configurations.Remove(configuration);
                await _botContext.SaveChangesAsync();
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
        Task<IEnumerable<ConfigurationDTO>> GetConfigurations(List<string> configs);
        Task<ConfigurationDTO> GetConfiguration(Guid configId);
        Task<bool> UpdateConfiguration(Guid configId, UpdateConfigurationDTO configuration);
        Task<bool> CreateConfiguration(UpdateConfigurationDTO configuration);
        Task<bool> DeleteConfiguration(Guid configId);
    }
}
