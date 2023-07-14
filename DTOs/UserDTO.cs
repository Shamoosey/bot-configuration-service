namespace DiscordBot_Backend.DTOs
{
    public class UserDTO
    {
        public Guid? Id { get; set; }
        public bool IsSecert { get; set; }
        public string UserName { get; set; }
        public string DiscordUserId { get; set; }
    }
}
