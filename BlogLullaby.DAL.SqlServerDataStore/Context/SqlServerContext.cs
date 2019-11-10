using BlogLullaby.DAL.DataStore.Entities;
using BlogLullaby.DAL.SqlServerDataStore.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BlogLullaby.DAL.SqlServerDataStore.Context
{
    public class SqlServerContext : DbContext
    {
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Dialog> Dialogs { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<DialogMember> DialogMembers { get; set; }
        public DbSet<PostBodyBlock> PostBodyBlocks { get; set; }
        public DbSet<NotReadMessage> NotReadMessages { get; set; }

        public SqlServerContext()
        {
            Database.EnsureCreated();
        }

        public SqlServerContext(DbContextOptions<SqlServerContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserProfileConfiguration());
            modelBuilder.ApplyConfiguration(new DialogMemberConfiguration());
            modelBuilder.ApplyConfiguration(new DialogConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfiguration(new PostBodyBlockConfiguration());
            modelBuilder.ApplyConfiguration(new MessageConfiguration());
            modelBuilder.ApplyConfiguration(new NotReadMessageConfiguration());
        }
    }
}