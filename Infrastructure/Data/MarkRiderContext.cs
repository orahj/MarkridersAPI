using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data
{
    public class MarkRiderContext : IdentityDbContext<AppUser,AspNetRole,Guid,AspNetUserClaim,AspNetUserRole,AspNetUserLogin,AspNetRoleClaim,AspNetUserToken>
    {
        public MarkRiderContext(DbContextOptions options) : base(options)
        {
        }
          protected override void OnModelCreating(ModelBuilder modelBuilder){
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // if(Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            // {
            //     foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            //     {
            //         var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));
            //         var dateTimeProperties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(DateTimeOffset));

            //         foreach (var property in properties)
            //         {
            //             modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();
            //         }
            //         foreach (var property in dateTimeProperties)
            //         {
            //             modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion(new DateTimeOffsetToBinaryConverter());
            //         }
            //     }
            // }
        }

        public DbSet<Delivery> Deliveries { get; set; }
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
        public DbSet<AdditionalCharges> AdditionalCharges { get; set;}
        public DbSet<DeliveryDetails> DeliveryDetails { get; set; }
        public DbSet<Ratings> Ratings { get; set; }
    }
}