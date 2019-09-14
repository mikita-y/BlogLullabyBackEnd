using BlogLullaby.DAL.DataStore.Entities;
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

        public SqlServerContext(DbContextOptions<SqlServerContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DialogMember>()
                .Property(x => x.FirstKey).HasColumnName("DialogId");
            modelBuilder.Entity<DialogMember>()
                .Property(x => x.SecondKey).HasColumnName("UserProfileId");
            modelBuilder.Entity<DialogMember>()
                .HasKey(x => new { x.FirstKey, x.SecondKey });
            modelBuilder.Entity<DialogMember>()
                .HasOne(x => x.Dialog)
                .WithMany(x => x.DialogMembers)
                .HasForeignKey(x => x.FirstKey);
            modelBuilder.Entity<DialogMember>()
                .HasOne(x => x.UserProfile)
                .WithMany(x => x.DialogMembers)
                .HasForeignKey(x => x.SecondKey);

            modelBuilder.Entity<UserProfile>()
                .HasIndex(x => x.Username)
                .IsUnique();

            
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
                //optionsBuilder.UseSqlServer(connectionString/* , b => b.MigrationsAssembly("BlogLullaby.DAL")*/);
        }  
    }
}