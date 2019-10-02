using BlogLullaby.DAL.DataStore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace BlogLullaby.DAL.SqlServerDataStore.Configurations
{
    class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.HasIndex(x => x.Username).IsUnique();
            builder.Property(x => x.Username).HasMaxLength(30);
            builder.Property(x => x.Username).IsRequired();
            builder.Property(x => x.FirstName).HasMaxLength(30);
            builder.Property(x => x.LastName).HasMaxLength(30);
            builder.Property(x => x.Specialization).HasMaxLength(60);
            builder.Property(x => x.City).HasMaxLength(30);
            builder.Property(x => x.IdentityUserId).HasMaxLength(450);
            builder.Property(x => x.IdentityUserId).IsRequired();
            builder.Property(x => x.LastVisit).HasDefaultValue(DateTime.Now);
            /*
            builder.HasData(
            new UserProfile { Id=0, Username="System", IdentityUserId=""});*/
        }
    }
}
