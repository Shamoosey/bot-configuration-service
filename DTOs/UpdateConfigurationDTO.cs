using System.Text.Json.Serialization;

namespace DiscordBot_Backend.DTOs
{
    public class UpdateConfigurationDTO
    {
        public string ServerId { get; set; }
        public string Name { get; set; }
        public string DefaultChannel { get; set; }
        public bool EnableKickCache { get; set; }
        public int KickCacheDays { get; set; }
        public int KickCacheHours { get; set; }
        public string KickCacheServerMessage { get; set; }
        public string KickCacheUserMessage { get; set; }
        public virtual List<UpdateUserDTO> Users { get; set; }
        public virtual List<UpdateTriggerDTO> Triggers { get; set; }
    }
}
