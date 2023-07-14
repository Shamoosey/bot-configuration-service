using AutoMapper;
using DiscordBot_Backend.Database.Models;
using DiscordBot_Backend.DTOs;

namespace DiscordBot_Backend
{
    public class  MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Configuration, ConfigurationDTO>()
                .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users))
                .ForMember(dest => dest.Triggers, opt => opt.MapFrom(src => src.Triggers))
                .ReverseMap();

            CreateMap<ConfigurationDTO, Configuration>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));

            CreateMap<StatusMessage, StatusMessageDTO>();

            CreateMap<UserDTO, User>().ReverseMap();

            CreateMap<User, UserDTO>();

            CreateMap<UserDTO, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));

            CreateMap<Trigger, TriggerDTO>()
                .ForMember(dest => dest.TriggerWords, opt => opt.MapFrom(src => src.TriggerWords.Select(x => x.Value)))
                .ForMember(dest => dest.TriggerResponses, opt => opt.MapFrom(src => src.TriggerResponses.Select(x => x.Value)))
                .ForMember(dest => dest.ReactEmotes, opt => opt.MapFrom(src => src.ReactEmotes.Select(x => x.Value)));

            CreateMap<TriggerDTO, Trigger>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));

            CreateMap<TriggerDTO, Trigger>();

            CreateMap<string, TriggerWord>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ConstructUsing(src => new TriggerWord { Value = src });

            CreateMap<string, ReactEmote>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ConstructUsing(src => new ReactEmote { Value = src });

            CreateMap<string, TriggerResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ConstructUsing(src => new TriggerResponse { Value = src });
        }
    }
}
