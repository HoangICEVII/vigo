﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vigo.Domain.Entity;

namespace vigo.Infrastructure.Configuration
{
    internal class ServiceConfiguration : IEntityTypeConfiguration<ServiceR>
    {
        public void Configure(EntityTypeBuilder<ServiceR> builder)
        {
            builder.ToTable("service");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.CreatedDate).HasColumnType("TIMESTAMP");
            builder.Property(c => c.UpdatedDate).HasColumnType("TIMESTAMP");
            builder.Property(c => c.DeletedDate).HasColumnType("TIMESTAMP");
            builder.HasMany<RoomService>().WithOne().HasForeignKey(e => e.ServiceId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
