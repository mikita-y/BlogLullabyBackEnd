using BlogLullaby.DAL.DataStore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogLullaby.DAL.SqlServerDataStore.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder
                .Property(x => x.Date)
                .HasDefaultValue(DateTime.Now);
        }
    }
}
