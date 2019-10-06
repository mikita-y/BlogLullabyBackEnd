using BlogLullaby.BLL.Infrastructure;
using BlogLullaby.BLL.UserCommunicatingService.DTO;
using BlogLullaby.DAL.DataStore.Entities;

namespace BlogLullaby.WEB_API.Models
{
    public class DialogModel
    {
        public DialogDTO Dialog { get; set; }
        public UserViewDTO Caller { get; set; }

        public DialogModel(DialogDTO dialog, UserViewDTO userView)
        {
            Dialog = dialog;
            Caller = userView;
        }

        public DialogModel(DialogDTO dialog, UserProfile userProfile)
        {
            Dialog = dialog;
            Caller = new UserViewDTO(userProfile);
        }
    }
}
