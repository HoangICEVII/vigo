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
    internal class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("invoice");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.CreatedDate).HasColumnType("TIMESTAMP");
            builder.Property(c => c.UpdatedDate).HasColumnType("TIMESTAMP");
            builder.Property(c => c.DeletedDate).HasColumnType("TIMESTAMP");
            builder.HasOne<Booking>().WithOne().HasForeignKey<Invoice>(e => e.BookingId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
