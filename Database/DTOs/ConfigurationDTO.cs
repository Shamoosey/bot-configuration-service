namespace Joebot_Backend.Database.DTOs
{
    public class ConfigurationDTO
    {
        public string ServerId { get; set; }
        public string Name { get; set; }
        public string DefaultChannel { get; set; }
        public bool EnableKickCache { get; set; }
        public int KickCacheDays { get; set; }
        public int KickCacheHours { get; set; }
        public string KickCacheServerMessage { get; set; }
        public string KickServerMessage { get; set; }
        public string KickUserMessage { get; set; }
        public virtual List<UserDTO> Users { get; set; }
        public virtual List<TriggerDTO> Triggers { get; set; }
        public virtual List<StatusMessageDTO> StatusMessages { get; set; }
    }
}
