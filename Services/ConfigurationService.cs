using AutoMapper;
using Joebot_Backend.Database;
using Joebot_Backend.Database.Models;
using Joebot_Backend.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Joebot_Backend.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly JoeContext _joeContext;
        private readonly IMapper _mapper;

        public ConfigurationService(
            JoeContext joeContext,
            IMapper mapper
        ) {
            _joeContext = joeContext;
            _mapper = mapper;
        }

        public async Task<bool> CreateConfiguration(ConfigurationDTO? configuration)
        {
            bool result = false;
            if(configuration != null)
            {
                var mappedConfig = _mapper.Map<ConfigurationDTO, Configuration>( configuration );
                var existingConfig = await _joeContext.Configurations.FirstOrDefaultAsync(x => x.ServerId == configuration.ServerId);
                if(existingConfig != null)
                {
                    throw new Exception("A record with that serverId already exists");
                }

                List<User> users = _mapper.Map<List<UserDTO>, List<User>>(configuration.Users);
                List<Trigger> triggers = new List<Trigger>();
                
                foreach (TriggerDTO triggerDto in configuration.Triggers)
                {
                    Trigger trigger = _mapper.Map<TriggerDTO, Trigger>(triggerDto);
                    trigger.TriggerResponses = new List<TriggerResponse>();
                    trigger.TriggerWords = new List<TriggerWord>();
                    trigger.ReactEmotes= new List<ReactEmote>();



                    triggers.Add(trigger);
                }

                mappedConfig.Triggers = triggers;
                mappedConfig.Users = users;

                await _joeContext.Configurations.AddAsync( mappedConfig );
                await _joeContext.SaveChangesAsync();
                result = true;
            }
            return result;
        }

        public async Task<bool> UpdateConfiguration(ConfigurationDTO? configuration)
        {
            return false;
        }

        public async Task<ConfigurationDTO?> GetConfiguration(string serverId) 
        {
            if(!String.IsNullOrEmpty(serverId))
            {
                var existingConfig = _joeContext.Configurations.FirstOrDefault(x => x.ServerId == serverId);
            } 
            else
            {
                throw new Exception("You need to provide a serverId");
            }
            return null;
        }
    }

    public interface IConfigurationService
    {
        Task<ConfigurationDTO> GetConfiguration(string serverId);
        Task<bool> UpdateConfiguration(ConfigurationDTO? configuration);
        Task<bool> CreateConfiguration(ConfigurationDTO? configuration);
    }
}
