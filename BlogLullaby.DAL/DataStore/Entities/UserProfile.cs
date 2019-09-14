using System;
using System.Collections.Generic;

namespace BlogLullaby.DAL.DataStore.Entities
{
    public class UserProfile : Entity<int>
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoUrl { get; set; }
        public string AvatarUrl { get; set; }
        public string Specialization { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public DateTime LastVisit { get; set; }
        public string IdentityUserId { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<DialogMember> DialogMembers { get; set; }
        public IEnumerable<Message> Messages { get; set; }
    }
}