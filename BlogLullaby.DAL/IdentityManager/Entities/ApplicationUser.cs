//using Microsoft.AspNetCore.Identity;

namespace BlogLullaby.DAL.IdentityManager.Entities
{
    public class ApplicationUser //: IdentityUser
    {
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }

        public string Id { get; set; }
        public string NormalizedEmail { get; set; }
        public string NormalizedUserName { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }


    }
}
