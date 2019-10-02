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
                .ForMember(m => m.Sender, opt => opt.MapFrom(x => new UserViewDTO(x.Sender)))
                .ForMember(m => m.DialogId, opt => opt.MapFrom(x => x.Dialog.Id));
            });
            var mapper = config.CreateMapper();
            return mapper.Map<Message, MessageDTO>(message);
        }

        public static Message MapToEntity(this MessageDTO messageDTO, UserProfile profile, Dialog dialog)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<MessageDTO, Message>()
                .ForMember(m => m.Sender, opt => opt.MapFrom(x => profile))
                .ForMember(m => m.Dialog, opt => opt.MapFrom(x => dialog))
                .ForMember(m => m.Date, opt => opt.MapFrom(x => x.Date == null ? DateTime.Now : x.Date));
            });
            var mapper = config.CreateMapper();
            return mapper.Map<MessageDTO, Message>(messageDTO);
        }

        public static MessageDTO MapToDTO(this Message message, bool isRead)
        {
            var messageDTO = message.MapToDTO();
            messageDTO.IsRead = isRead;
            return messageDTO;
        }
    }
}
