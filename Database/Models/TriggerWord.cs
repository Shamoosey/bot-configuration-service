using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Joebot_Backend.Database.Models
{
    public class TriggerWord
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        
        [Required]
        public string Value { get; set; }

        public virtual Trigger Trigger { get; set; }

    }
}
