using BlogLullaby.DAL.DataStore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace BlogLullaby.DAL.SqlServerDataStore.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder
                .Property(x => x.Title)
                .HasMaxLength(500);

            builder
                .Property(x => x.Date)
                .HasDefaultValue(DateTime.Now);

            builder
                .Property(x => x.Visits)
                .HasDefaultValue(0);
        }
    }
}
