using AutoMapper;
using Joebot_Backend.Database;
using Joebot_Backend.DTOs;

namespace Joebot_Backend.Services
{
    public class StatusService : IStatusService
    {
        private readonly JoeContext _joeContext;
        private readonly IMapper _mapper;

        public StatusService (
            JoeContext joeContext,
            IMapper mapper
        ) {
            _joeContext = joeContext;
            _mapper = mapper;
        }

        public Task<bool> CreateStatusMessage(StatusMessageDTO statusMessage)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteStatusMessage(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<StatusMessageDTO>> GetStatusMessages()
        {
            throw new NotImplementedException();
        }
    }

    public interface IStatusService
    {
        public Task<bool> CreateStatusMessage(StatusMessageDTO statusMessage);
        public Task<bool> DeleteStatusMessage(Guid id);
        public Task<List<StatusMessageDTO>> GetStatusMessages();
    }
}
