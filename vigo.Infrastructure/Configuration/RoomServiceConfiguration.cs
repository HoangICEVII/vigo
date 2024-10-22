using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vigo.Domain.Entity;

namespace vigo.Infrastructure.Configuration
{
    internal class RoomServiceConfiguration : IEntityTypeConfiguration<RoomService>
    {
        public void Configure(EntityTypeBuilder<RoomService> builder)
        {
            builder.ToTable("roomService");
            builder.HasNoKey();
        }
    }
}
