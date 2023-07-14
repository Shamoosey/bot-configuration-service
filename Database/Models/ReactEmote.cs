using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiscordBot_Backend.Database.Models
{
    public class ReactEmote
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Value { get; set; }

        public virtual Trigger Trigger { get; set; }
    }
}
