﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Joebot_Backend.Database.Models
{
    public class Configuration
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string ServerId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string DefaultChannel { get; set; }


        public bool EnableKickCache { get; set; }

        public int KickCacheDays { get; set; }

        public int KickCacheHours { get; set; }

        public string KickCacheServerMessage { get; set; }

        public string KickServerMessage { get; set; }

        public string KickUserMessage { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Trigger> Triggers { get; set; }
        public virtual ICollection<StatusMessage> StatusMessages { get; set; }
    }
}
