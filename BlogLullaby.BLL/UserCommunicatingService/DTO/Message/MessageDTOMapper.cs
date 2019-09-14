using BlogLullaby.BLL.Infrastructure;
using BlogLullaby.DAL.DataStore.Entities;
using System;

namespace BlogLullaby.BLL.UserCommunicatingService.DTO
{
    public static class MessageDTOMapper
    {
        public static MessageDTO MapToDTO(this Message message)
        {
            var messageDTO = new MessageDTO()
            {
                Body = message.Body,
                Date = message.Date,
                DialogId = message.Dialog.Id
            };
            //if (message.UserProfile != null)
                messageDTO.Owner = new UserViewDTO(message.UserProfile);
            return messageDTO;
        }

        public static Message MapToEntity(this MessageDTO messageDTO, UserProfile profile, Dialog dialog)
        {
            var message = new Message()
            {
                Body = messageDTO.Body,
                Date = messageDTO.Date,
                UserProfile = profile,
                Dialog = dialog
            };
            if (message.Date == null)
                message.Date = DateTime.Now;
            return message;
        }
    }
}
