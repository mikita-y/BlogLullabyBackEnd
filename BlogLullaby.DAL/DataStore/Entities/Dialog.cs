using System.Collections.Generic;

namespace BlogLullaby.DAL.DataStore.Entities
{
    public class Dialog : Entity<string>
    {
        public string DialogName { get; set; }
        public IEnumerable<DialogMember> DialogMembers {get; set;}
        public IEnumerable<Message> Messages { get; set; }
    }
}
