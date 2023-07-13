using AutoMapper;
using Joebot_Backend.Database;
using Joebot_Backend.Database.Models;
using Joebot_Backend.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Joebot_Backend.Services
{
    public class TriggerService : ITriggerService
    {
        private readonly ILogger<TriggerService> _logger;
        private readonly JoeContext _joeContext;
        private readonly IMapper _mapper;

        public TriggerService(JoeContext joeContext, IMapper mapper)
        {
            _joeContext = joeContext;
            _mapper = mapper;
        }

        public async Task<bool> CreateTrigger(string serverId, TriggerDTO Trigger)
        {
            bool result = false;
            try
            {
                var existingConfiguration = _joeContext.Configurations.FirstOrDefault(x => x.ServerId == serverId);

                if (existingConfiguration == null) 
                {
                    throw new Exception($"No configuration associated with serverId: {serverId}");
                }

                var triggerEntity = _mapper.Map<TriggerDTO, Trigger>(Trigger);
                var reactEmotes = _mapper.Map<List<ReactEmote>>(Trigger.ReactEmotes);
                var triggerWords = _mapper.Map<List<TriggerWord>>(Trigger.TriggerWords);
                var triggerResponses = _mapper.Map<List<TriggerResponse>>(Trigger.TriggerResponses);

                triggerEntity.ReactEmotes= reactEmotes;
                triggerEntity.TriggerWords = triggerWords;
                triggerEntity.TriggerResponses = triggerResponses;

                triggerEntity.Configuration = existingConfiguration;
                _joeContext.Triggers.Add(triggerEntity);
                await _joeContext.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                // todo - add logging
                Console.WriteLine(ex);
                result = false;
            }

            return result;
        }

        public async Task<bool> UpdateTrigger(Guid id, TriggerDTO editTrigger)
        {
            bool result = false;
            try
            {
                var triggerEntity = await _joeContext.Triggers
                    .Include(c => c.TriggerWords)
                    .Include(c => c.TriggerResponses)
                    .Include(c => c.ReactEmotes)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (triggerEntity == null)
                {
                    throw new Exception("Trigger doesn't exist");
                }

                // Update the properties of the trigger entity
                triggerEntity.MessageDelete = editTrigger.MessageDelete;
                triggerEntity.SendRandomResponse = editTrigger.SendRandomResponse;
                triggerEntity.IgnoreCooldown = editTrigger.IgnoreCooldown;

                // Update the trigger words
                if(triggerEntity.TriggerWords.Count > 0)
                {
                    triggerEntity.TriggerWords.Clear();
                }
                foreach (var triggerWordValue in editTrigger.TriggerWords)
                {
                    triggerEntity.TriggerWords.Add(new TriggerWord { Value = triggerWordValue });
                }

                // Update the react emotes
                if(triggerEntity.ReactEmotes.Count > 0)
                {
                    triggerEntity.ReactEmotes.Clear();
                }
                foreach (var reactEmoteValue in editTrigger.ReactEmotes)
                {
                    triggerEntity.ReactEmotes.Add(new ReactEmote { Value = reactEmoteValue });
                }

                // Update the trigger responses
                if(triggerEntity.TriggerResponses.Count > 0)
                {
                    triggerEntity.TriggerResponses.Clear();
                }
                foreach (var triggerResponseValue in editTrigger.TriggerResponses)
                {
                    triggerEntity.TriggerResponses.Add(new TriggerResponse { Value = triggerResponseValue });
                }

                await _joeContext.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                // todo - add logging
                Console.WriteLine(ex);
                result = false;
            }

            return result;
        }

        public async Task<TriggerDTO?> GetTrigger(Guid id)
        {
            TriggerDTO? triggerResult = null;
            try
            {
                var triggerEntity = await _joeContext.Triggers
                    .Include(c => c.TriggerWords)
                    .Include(c => c.TriggerResponses)
                    .Include(c => c.ReactEmotes)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (triggerEntity == null)
                {
                    throw new Exception("Trigger doesn't exist");
                }

                var triggerDTO = _mapper.Map<Trigger, TriggerDTO>(triggerEntity);

                triggerResult = triggerDTO;
            }
            catch (Exception ex)
            {
                // todo - add logging
                Console.WriteLine(ex);
            }
                return triggerResult;
        }

        public async Task<bool> DeleteTrigger(Guid id)
        {
            bool result = false;
            try
            {
                var statusEntity = await _joeContext.Triggers
                    .Include(c => c.TriggerWords)
                    .Include(c => c.TriggerResponses)
                    .Include(c => c.ReactEmotes)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (statusEntity == null)
                {
                    throw new Exception("Trigger does not exist");
                }

                _joeContext.Triggers.Remove(statusEntity);
                await _joeContext.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                // todo - add logging
                Console.WriteLine(ex);
                result = false;
            }

            return result;
        }

        public async Task<List<TriggerDTO>> GetTriggers(string serverId)
        {
            try
            {
                var triggerEntities = await _joeContext.Triggers
                    .Include(c => c.TriggerWords)
                    .Include(c => c.TriggerResponses)
                    .Include(c => c.ReactEmotes)
                    .Where(x => x.Configuration.ServerId == serverId)
                    .ToListAsync();
                var triggerDTOs = _mapper.Map<List<Trigger>, List<TriggerDTO>>(triggerEntities);

                return triggerDTOs;
            }
            catch (Exception ex)
            {
                // todo - add logging
                Console.WriteLine(ex);
                return new List<TriggerDTO>();
            }
        }
    }

    public interface ITriggerService
    {
        public Task<bool> CreateTrigger(string serverId, TriggerDTO Trigger);
        public Task<bool> DeleteTrigger(Guid id);
        public Task<bool> UpdateTrigger(Guid id, TriggerDTO TriggerDTO);
        public Task<List<TriggerDTO>> GetTriggers(string serverId);
        public Task<TriggerDTO> GetTrigger(Guid id);
    }
}