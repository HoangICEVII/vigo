using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vigo.Domain.Entity;
using vigo.Domain.User;

namespace vigo.Infrastructure.Configuration
{
    internal class DistrictConfiguration : IEntityTypeConfiguration<District>
    {
        public void Configure(EntityTypeBuilder<District> builder)
        {
            builder.ToTable("district");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.CreatedDate).HasColumnType("TIMESTAMP");
            builder.Property(c => c.UpdatedDate).HasColumnType("TIMESTAMP");
            builder.Property(c => c.DeletedDate).HasColumnType("TIMESTAMP");
            builder.HasOne<Province>().WithMany().HasForeignKey(e => e.ProvinceId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
