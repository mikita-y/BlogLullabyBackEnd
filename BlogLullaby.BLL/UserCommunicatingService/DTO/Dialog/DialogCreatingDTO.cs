using BlogLullaby.BLL.Infrastructure;
using BlogLullaby.DAL.DataStore.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BlogLullaby.BLL.UserCommunicatingService.DTO
{
    public class DialogCreatingDTO: DTOobject
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public IEnumerable<string> Members { get; set; } 

        public Dialog GetDialogEntity(IEnumerable<UserProfile> profiles)
        {
            var dialog = new Dialog()
            {
                DialogName = Title
            };
            dialog.DialogMembers = profiles.Select(x => new DialogMember() { Dialog = dialog, UserProfile = x });
            return dialog;
        }
    }
}
