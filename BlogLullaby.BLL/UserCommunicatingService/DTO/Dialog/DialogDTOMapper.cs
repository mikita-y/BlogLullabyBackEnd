using BlogLullaby.BLL.Infrastructure;
using BlogLullaby.DAL.DataStore.Entities;
using System.Linq;

namespace BlogLullaby.BLL.UserCommunicatingService.DTO
{
    public static class DialogDTOMapper
    {
        public static DialogDTO MapToDTO(this Dialog dialog)
        {
            var dialogDTO = new DialogDTO()
            {
                Id = dialog.Id,
                Title = dialog.DialogName,
            };
            if (dialog.Messages != null && dialog.Messages.Count() > 0)
                dialogDTO.Messages = dialog.Messages.Select(x => x.MapToDTO()).OrderBy(x => x.Date);
            if (dialog.DialogMembers != null && dialog.DialogMembers.Count() > 0)
                dialogDTO.Members = dialog.DialogMembers.Select(x => new UserViewDTO(x.UserProfile));
            return dialogDTO;
        }

        public static DialogView MapToView(this Dialog dialog)
        {
            var dialogView = new DialogView()
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
    }
}
