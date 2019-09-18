using BlogLullaby.DAL.DataStore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogLullaby.DAL.SqlServerDataStore.Configurations
{
    public class DialogMemberConfiguration : IEntityTypeConfiguration<DialogMember>
    {
        public void Configure(EntityTypeBuilder<DialogMember> modelBuilder)
        {
            modelBuilder.Property(x => x.FirstKey).HasColumnName("DialogId");
            modelBuilder.Property(x => x.SecondKey).HasColumnName("UserProfileId");
            modelBuilder.HasKey(x => new { x.FirstKey, x.SecondKey });

            modelBuilder
                .HasOne(x => x.Dialog)
                .WithMany(x => x.DialogMembers)
                .HasForeignKey(x => x.FirstKey);

            modelBuilder
                .HasOne(x => x.UserProfile)
                .WithMany(x => x.DialogMembers)
                .HasForeignKey(x => x.SecondKey);
        }
    }
}
