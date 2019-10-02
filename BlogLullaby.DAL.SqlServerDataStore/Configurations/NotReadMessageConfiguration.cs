using BlogLullaby.DAL.DataStore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogLullaby.DAL.SqlServerDataStore.Configurations
{
    public class NotReadMessageConfiguration : IEntityTypeConfiguration<NotReadMessage>
    {
        public void Configure(EntityTypeBuilder<NotReadMessage> modelBuilder)
        {
            modelBuilder.Property(x => x.FirstKey).HasColumnName("MessageId");
            modelBuilder.Property(x => x.SecondKey).HasColumnName("RecipientId");
            modelBuilder.HasKey(x => new { x.FirstKey, x.SecondKey });
            modelBuilder.Ignore(x => x.Message);
            modelBuilder.Ignore(x => x.Recipient);
        }
    }
}
