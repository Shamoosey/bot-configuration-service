using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Joebot_Backend.Database.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public bool IsSecert { get; set; } = false;
        [Required]
        public string UserName { get; set; }
        [Required]
        public string DiscordUserId { get; set; }
        public virtual Configuration Configuration { get; set; }
    }
}
