using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiscordBot_Backend.Database.Models
{
    public class StatusMessage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string? Status { get; set; }

        [Required]
        public int Type { get; set; }
    }
}
