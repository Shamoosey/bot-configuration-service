using AutoMapper;
using DiscordBot_Backend.Database;
using DiscordBot_Backend.Database.Models;
using DiscordBot_Backend.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DiscordBot_Backend.Services
{
    public class StatusService : IStatusService
    {
        private readonly BotContext _botContext;
        private readonly IMapper _mapper;

        public StatusService(BotContext botContext, IMapper mapper)
        {
            _botContext = botContext;
            _mapper = mapper;
        }

        public async Task<bool> CreateStatusMessage(string statusMessage, int statusType)
        {
            bool result = false;
            try
            {
                var statusEntity = new StatusMessage()
                {
                    Id = Guid.NewGuid(),
                    Status = statusMessage,
                    Type = statusType
                };
                _botContext.StatusMessages.Add(statusEntity);
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

        public async Task<bool> DeleteStatusMessage(Guid id)
        {
            bool result = false;
            try
            {
                var statusEntity = await _botContext.StatusMessages.FindAsync(id);

                if (statusEntity == null)
                {
                    throw new Exception("Status does not exist");
                }

                _botContext.StatusMessages.Remove(statusEntity);
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

        public async Task<List<StatusMessageDTO>> GetStatusMessages()
        {
            try
            {
                var statusEntities = await _botContext.StatusMessages.ToListAsync();
                var statusDTOs = _mapper.Map<List<StatusMessageDTO>>(statusEntities);

                return statusDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not get status messages", ex);
            }
        }
    }

    public interface IStatusService
    {
        public Task<bool> CreateStatusMessage(string statusMessage, int statusType);
        public Task<bool> DeleteStatusMessage(Guid id);
        public Task<List<StatusMessageDTO>> GetStatusMessages();
    }
}