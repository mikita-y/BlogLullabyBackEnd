using BlogLullaby.BLL.Infrastructure;
using BlogLullaby.DAL.DataStore.Entities;
using System.Linq;

namespace BlogLullaby.BLL.UserCommunicatingService.DTO
{
    public static class DialogDTOMapper
    {
        public static DialogDTO MapToDTO(this Dialog dialog)
        {
            var dialogDTO = MapToDTOPrivateRealization(dialog);
            if (dialog.Messages != null && dialog.Messages.Count() > 0)
                dialogDTO.Messages = dialog.Messages.Select(x => x.MapToDTO()).OrderBy(x => x.Date);
            return dialogDTO;
        }

        public static DialogPreview MapToPreview(this Dialog dialog)
        {
            //пересечение множеств
            var dialogView = new DialogPreview()
            {
                Id = dialog.Id,
                Title = dialog.DialogName,
            };
            if (dialog.Messages != null && dialog.Messages.Count() > 0)
            {
                var LastMessageDate = dialog.Messages.Max(x => x.Date);
                dialogView.LastMessage = dialog.Messages.FirstOrDefault(x => x.Date == LastMessageDate).MapToDTO();
            }
            return dialogView;
        }

        public static DialogPreview MapToPreview(this Dialog dialog, IQueryable<NotReadMessage> notReadMessages)
        {
            var dialogView = dialog.MapToPreview();
            dialogView.UnReadMessages = notReadMessages
                .Select(x => x.FirstKey)
                .Intersect(dialog.Messages.Select(x => x.Id)) //пересечение множеств с одинаковыми id
                .Count();
            return dialogView;
        }

        public static DialogDTO MapToDTO(this Dialog dialog, IQueryable<NotReadMessage> notReadMessages)
        {
            var dialogDTO = MapToDTOPrivateRealization(dialog);
                dialogDTO.Messages = dialog.Messages.
                    Select(x => x.MapToDTO(notReadMessages.FirstOrDefault(y => y.FirstKey == x.Id) == null))
                    .OrderBy(x => x.Date);
            return dialogDTO;
        }

        private static DialogDTO MapToDTOPrivateRealization(Dialog dialog)
        {
            var dialogDTO = new DialogDTO()
            {
                Id = dialog.Id,
                Title = dialog.DialogName,
            };
            if (dialog.DialogMembers != null && dialog.DialogMembers.Count() > 0)
                dialogDTO.Members = dialog.DialogMembers.Select(x => new UserViewDTO(x.UserProfile));
            return dialogDTO;
        }
    }
}
