using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class MarkRiderContext : DbContext
    {
        public MarkRiderContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Delivery> Deliveries { get; set; }
    }
}