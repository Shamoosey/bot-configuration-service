using AutoMapper;
using DiscordBot_Backend.Database;
using DiscordBot_Backend.Database.Models;
using DiscordBot_Backend.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DiscordBot_Backend.Services
{
    public class TriggerService : ITriggerService
    {
        private readonly ILogger<TriggerService> _logger;
        private readonly BotContext _botContext;
        private readonly IMapper _mapper;

        public TriggerService(BotContext botContext, IMapper mapper)
        {
            _botContext = botContext;
            _mapper = mapper;
        }

        public async Task<bool> CreateTrigger(Guid configId, UpdateTriggerDTO Trigger)
        {
            bool result = false;
            try
            {
                var existingConfiguration = _botContext.Configurations.FirstOrDefault(x => x.Id == configId);

                if (existingConfiguration == null) 
                {
                    throw new Exception($"No configuration associated with id: {configId}");
                }

                var triggerEntity = _mapper.Map<UpdateTriggerDTO, Trigger>(Trigger);
                var reactEmotes = _mapper.Map<List<ReactEmote>>(Trigger.ReactEmotes);
                var triggerWords = _mapper.Map<List<TriggerWord>>(Trigger.TriggerWords);
                var triggerResponses = _mapper.Map<List<TriggerResponse>>(Trigger.TriggerResponses);

                triggerEntity.ReactEmotes= reactEmotes;
                triggerEntity.TriggerWords = triggerWords;
                triggerEntity.TriggerResponses = triggerResponses;

                triggerEntity.Configuration = existingConfiguration;
                _botContext.Triggers.Add(triggerEntity);
                await _botContext.SaveChangesAsync();

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

        public async Task<bool> UpdateTrigger(Guid id, UpdateTriggerDTO editTrigger)
        {
            bool result = false;
            try
            {
                var triggerEntity = await _botContext.Triggers
                    .Include(c => c.TriggerWords)
                    .Include(c => c.TriggerResponses)
                    .Include(c => c.ReactEmotes)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (triggerEntity == null)
                {
                    throw new Exception("Trigger doesn't exist");
                }

                // Update the properties of the trigger entity
                triggerEntity.Name = editTrigger.Name;
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

                await _botContext.SaveChangesAsync();

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
                var triggerEntity = await _botContext.Triggers
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
                var triggerEntity = await _botContext.Triggers
                    .Include(c => c.TriggerWords)
                    .Include(c => c.TriggerResponses)
                    .Include(c => c.ReactEmotes)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (triggerEntity == null)
                {
                    throw new Exception("Trigger does not exist");
                }

                _botContext.Triggers.Remove(triggerEntity);
                await _botContext.SaveChangesAsync();

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

        public async Task<List<TriggerDTO>> GetTriggers(Guid configId)
        {
            try
            {
                var triggerEntities = await _botContext.Triggers
                    .Include(c => c.TriggerWords)
                    .Include(c => c.TriggerResponses)
                    .Include(c => c.ReactEmotes)
                    .Where(x => x.Configuration.Id == configId)
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
        public Task<bool> CreateTrigger(Guid configId, UpdateTriggerDTO Trigger);
        public Task<bool> DeleteTrigger(Guid id);
        public Task<bool> UpdateTrigger(Guid id, UpdateTriggerDTO TriggerDTO);
        public Task<List<TriggerDTO>> GetTriggers(Guid configId);
        public Task<TriggerDTO> GetTrigger(Guid id);
    }
}