namespace DiscordBot_Backend.DTOs
{
    public class UpdateUserDTO
    {
        public bool IsSecert { get; set; }
        public string UserName { get; set; }
        public string DiscordUserId { get; set; }
    }
}
