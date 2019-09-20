using System;

namespace BlogLullaby.BLL.UserListService.DTO
{
    public class UserProfilePreviewDTO
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public string AvatarUrl { get; set; }
        public string Specialization { get; set; }
        public string City { get; set; }
        public DateTime LastVisit { get; set; }
        public int TotalVisits { get; set; }
    }
}
