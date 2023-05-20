namespace Joebot_Backend.Database.DTOs
{
    public class StatusMessageDTO
    {
        public string? Status { get; set; }
        public int Type { get; set; }
        public ConfigurationDTO Configuration { get; set; }
    }
}
