using BlogLullaby.DAL.DataStore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogLullaby.DAL.SqlServerDataStore.Configurations
{
    public class PostBodyBlockConfiguration : IEntityTypeConfiguration<PostBodyBlock>
    {
        public void Configure(EntityTypeBuilder<PostBodyBlock> builder)
        {
            builder.Property("PostId").IsRequired();
        }
    }
}
