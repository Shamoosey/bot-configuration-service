﻿using System.Text.Json.Serialization;

namespace Joebot_Backend.DTOs
{
    public class JoeConfigDTO
    {
        public Guid? Id { get; set; }
        public string ServerId { get; set; }
        public string Name { get; set; }
        public string DefaultChannel { get; set; }
        public bool EnableKickCache { get; set; }
        public int KickCacheDays { get; set; }
        public int KickCacheHours { get; set; }
        public string KickCacheServerMessage { get; set; }
        public string KickServerMessage { get; set; }
        public string KickUserMessage { get; set; }
        public virtual List<UserDTO> Users { get; set; }
        public virtual List<TriggerDTO> Triggers { get; set; }
    }
}