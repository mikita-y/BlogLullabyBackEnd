using AutoMapper;
using BlogLullaby.BLL.Infrastructure;
using BlogLullaby.DAL.DataStore.Entities;
using System;

namespace BlogLullaby.BLL.UserCommunicatingService.DTO
{
    public static class MessageDTOMapper
    {
        public static MessageDTO MapToDTO(this Message message)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Message, MessageDTO>()
                .ForMember("Owner", opt => opt.MapFrom(x => new UserViewDTO(x.UserProfile)))
                .ForMember("DialogId", opt => opt.MapFrom(x => x.Dialog.Id));
            });
            var mapper = config.CreateMapper();
            return mapper.Map<Message, MessageDTO>(message);
        }

        public static Message MapToEntity(this MessageDTO messageDTO, UserProfile profile, Dialog dialog)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<MessageDTO, Message>()
                .ForMember("UserProfile", opt => opt.MapFrom(x => profile))
                .ForMember("Dialog", opt => opt.MapFrom(x => dialog))
                .ForMember("Date", opt => opt.MapFrom(x => x.Date == null ? DateTime.Now : x.Date));
            });
            var mapper = config.CreateMapper();
            return mapper.Map<MessageDTO, Message>(messageDTO);
        }
    }
}
