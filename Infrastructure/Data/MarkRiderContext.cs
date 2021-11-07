using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<DeliveryLocation> DeliveryLocations { get; set; }
        public DbSet<DeliveryDistance> DeliveryDistances {get; set; }
        public DbSet<FileData> FileDatas { get;set; }
        public DbSet<Notification> Notifications {get; set; }
        public DbSet<Payment> Payments {get; set; }
        public DbSet<Rider> Riders {get; set; }
        public DbSet<RiderGuarantor> RiderGuarantors {get; set; }
        public DbSet<RidersDelivery> RidersDeliveries {get; set; }
        public DbSet<Transaction> Transactions {get;set;}
        public DbSet<Wallet> Wallets {get; set; }
        public DbSet<DeliveryItem> DeliveryItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}