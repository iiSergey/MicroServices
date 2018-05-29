using AutoMapper;
using MessagesService.Models;
using MessagesService.Models.Dto;
using System;

namespace MessagesService.Settings
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Message, MessageOutputModel>();
            CreateMap<MessageInputModel, Message>();
            CreateMap<MessageCreateInputModel, Message>()
                .ForMember(x => x.CreatedOn, opt => opt.MapFrom(x => DateTime.UtcNow));
        }
    }
}