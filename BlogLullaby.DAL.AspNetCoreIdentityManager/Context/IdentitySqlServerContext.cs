using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogLullaby.DAL.AspNetCoreIdentityManager.Context
{
    public class IdentitySqlServerContext : IdentityDbContext<IdentityUser>
    {

        public IdentitySqlServerContext()
        {
            Database.EnsureCreated();
        }

        public IdentitySqlServerContext(DbContextOptions<IdentitySqlServerContext> options)
            : base(options)
        {
        }
    }
}