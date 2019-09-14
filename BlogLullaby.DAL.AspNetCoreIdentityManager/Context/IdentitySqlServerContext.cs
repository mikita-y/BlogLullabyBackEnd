using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace logLullaby.DAL.AspNetCoreIdentityManager.Context
{
    public class IdentitySqlServerContext : IdentityDbContext<IdentityUser>
    {
        
        public IdentitySqlServerContext(DbContextOptions<IdentitySqlServerContext> options)
            : base(options)
        {
        }

        public IdentitySqlServerContext()
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BlogLullabyIdentity;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                optionsBuilder.UseSqlServer(connectionString);
        } 
    }
}