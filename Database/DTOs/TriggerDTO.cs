namespace Joebot_Backend.Database.DTOs
{
    public class TriggerDTO
    {
        public bool MessageDelete { get; set; }
        public bool SendRandomResponse { get; set; }
        public bool IgnoreCooldown { get; set; }
        public List<string> TriggerWords { get; set; }
        public List<string> ReactEmotes { get; set; }
        public List<string> TriggerResponses { get; set; }
    }
}
