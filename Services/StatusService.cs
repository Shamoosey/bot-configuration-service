using AutoMapper;
using Joebot_Backend.Database;
using Joebot_Backend.Database.Models;
using Joebot_Backend.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Joebot_Backend.Services
{
    public class StatusService : IStatusService
    {
        private readonly JoeContext _joeContext;
        private readonly IMapper _mapper;

        public StatusService(JoeContext joeContext, IMapper mapper)
        {
            _joeContext = joeContext;
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
                _joeContext.StatusMessages.Add(statusEntity);
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

        public async Task<bool> DeleteStatusMessage(Guid id)
        {
            bool result = false;
            try
            {
                var statusEntity = await _joeContext.StatusMessages.FindAsync(id);

                if (statusEntity == null)
                {
                    throw new Exception("Status does not exist");
                }

                _joeContext.StatusMessages.Remove(statusEntity);
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

        public async Task<List<StatusMessageDTO>> GetStatusMessages()
        {
            try
            {
                var statusEntities = await _joeContext.StatusMessages.ToListAsync();
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