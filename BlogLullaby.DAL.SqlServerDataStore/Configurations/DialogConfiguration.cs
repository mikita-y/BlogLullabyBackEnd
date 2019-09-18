using BlogLullaby.DAL.DataStore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogLullaby.DAL.SqlServerDataStore.Configurations
{
    class DialogConfiguration : IEntityTypeConfiguration<Dialog>
    {
        public void Configure(EntityTypeBuilder<Dialog> builder)
        {
            builder
                .Property(x => x.DialogName).HasMaxLength(100);
        }
    }
}
