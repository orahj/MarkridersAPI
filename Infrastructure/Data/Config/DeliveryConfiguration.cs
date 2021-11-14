using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class DeliveryConfiguration : IEntityTypeConfiguration<Delivery>
    {
        public void Configure(EntityTypeBuilder<Delivery> builder)
        {
            builder.Property(p =>p.Id).IsRequired();
            builder.Property(p=>p.DeliveryNo).IsRequired().HasMaxLength(15);
            builder.Property(p => p.TotalAmount).HasColumnType("decimal(18,2)");
            //builder.HasMany(d => d.DeliveryItems).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}