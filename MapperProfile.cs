using AutoMapper;
using Joebot_Backend.Database.DTOs;
using Joebot_Backend.Database.Models;

namespace Joebot_Backend
{
    public class  MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<ConfigurationDTO, Configuration>()
                .ForMember(dest => dest.ServerId, opt => opt.MapFrom(src => src.ServerId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.DefaultChannel, opt => opt.MapFrom(src => src.DefaultChannel))
                .ForMember(dest => dest.EnableKickCache, opt => opt.MapFrom(src => src.EnableKickCache))
                .ForMember(dest => dest.KickCacheDays, opt => opt.MapFrom(src => src.KickCacheDays))
                .ForMember(dest => dest.KickCacheHours, opt => opt.MapFrom(src => src.KickCacheHours))
                .ForMember(dest => dest.KickCacheServerMessage, opt => opt.MapFrom(src => src.KickCacheServerMessage))
                .ForMember(dest => dest.KickServerMessage, opt => opt.MapFrom(src => src.KickServerMessage))
                .ForMember(dest => dest.KickUserMessage, opt => opt.MapFrom(src => src.KickUserMessage));

            CreateMap<UserDTO, User> ()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.DiscordUserId, opt => opt.MapFrom(src => src.DiscordUserId))
                .ForMember(dest => dest.IsSecert, opt => opt.MapFrom(src => src.IsSecert));

            CreateMap<TriggerDTO, Trigger>()
                .ForMember(dest => dest.SendRandomResponse, opt => opt.MapFrom(src => src.SendRandomResponse))
                .ForMember(dest => dest.MessageDelete, opt => opt.MapFrom(src => src.MessageDelete))
                .ForMember(dest => dest.IgnoreCooldown, opt => opt.MapFrom(src => src.IgnoreCooldown));

            CreateMap<StatusMessageDTO, StatusMessage>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type)); 
        }
    }
}
