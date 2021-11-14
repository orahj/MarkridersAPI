using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class DeliveryItemConfiguration : IEntityTypeConfiguration<DeliveryItem>
    {
        public void Configure(EntityTypeBuilder<DeliveryItem> builder)
        {
            builder.Property(s => s.DeliveryStatus)
                .HasConversion(
                    o => o.ToString(),
                    o => (DeliveryStatus) Enum.Parse(typeof(DeliveryStatus),o)
                );
            
            builder.Property(s => s.Carriers)
                .HasConversion(
                    o => o.ToString(),
                    o => (Carriers) Enum.Parse(typeof(Carriers),o)
                );
            builder.Property(s => s.DeliveryTime)
                .HasConversion(
                    o => o.ToString(),
                    o => (DeliveryTime) Enum.Parse(typeof(DeliveryTime),o)
                );
             builder.Property(s => s.DeliveryTpe)
                .HasConversion(
                    o => o.ToString(),
                    o => (DeliveryTpe) Enum.Parse(typeof(DeliveryTpe),o)
                );
            builder.Property(d => d.DeliveryAmount)
                .HasColumnType("decimal(18,2)");
    }
}
}