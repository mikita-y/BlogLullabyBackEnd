using System.ComponentModel.DataAnnotations.Schema;

namespace BlogLullaby.DAL.DataStore.Entities
{
    public class DialogMember: EntityWithCompositeKey<string, int>
    {
        //[ForeignKey("DialogId")]
        public Dialog Dialog { get; set; }
        //[ForeignKey("UserProfileId")]
        public UserProfile UserProfile { get; set; }
    }
}
