using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiscordBot_Backend.Database.Models
{
    public class Trigger
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public bool MessageDelete { get; set; }

        public bool SendRandomResponse { get; set; }

        public bool IgnoreCooldown { get; set; }

        public virtual Configuration Configuration { get; set; }

        public virtual ICollection<TriggerWord> TriggerWords { get; set; }

        public virtual ICollection<ReactEmote> ReactEmotes { get; set; }

        public virtual ICollection<TriggerResponse> TriggerResponses { get; set; }
    }
}
