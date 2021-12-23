using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Enum;
using Microsoft.Extensions.Logging;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data
{
    public class MarkRiderContextSeed
    {

        public static async Task SeedAsync(MarkRiderContext context, ILoggerFactory loggerFactory)
        {
            try{
                if(!context.States.Any())
                   await SeedState(context);
                if(!context.Countries.Any())
                   await SeedCountry(context);
                if(!context.Deliveries.Any())
                   await SeedDelivery(context);
                if(!context.FileDatas.Any())
                    await SeedFIle(context);
                if(!context.DeliveryLocations.Any())
                   await SeedDeliveryLocation(context);
                if(!context.DeliveryItems.Any())
                   await SeedDeliveryItems(context);
                if(!context.DeliveryDistances.Any())
                   await SeedDeliveryDistance(context);
                if(!context.AdditionalCharges.Any())
                    await SeedAdditionalCharges(context);
                if (!context.DeliveryCancelationReasons.Any())
                    await SeedDeliverycancelation(context);
            }
            catch(Exception ex)
            {
                var logger = loggerFactory.CreateLogger<MarkRiderContext>();
                logger.LogError(ex.Message);
            }
        }
        public static async Task SeedState(MarkRiderContext context)
        {
            var state = new[]
            {
                new State { Code="AB", Name ="Abia" },
                new State { Code="AD", Name ="Adamawa" },
                new State { Code="AK", Name ="Akwa Ibom" },
                new State { Code="AN", Name ="Anambra" },
                new State { Code="BA", Name ="Bauchi" },
                new State { Code="BE", Name ="Benue" },
                new State { Code="BO", Name ="Borno" },
                new State { Code="BY", Name ="Bayelsa" },
                new State { Code="CR", Name ="Cross Rive" },
                new State { Code="DT", Name ="Delta" },
                new State { Code="EB", Name ="Ebonyi" },
                new State { Code="ED", Name ="Edo" },
                new State { Code="EN", Name ="Enugu" },
                new State { Code="ET", Name ="Ekiti" },
                new State { Code="FCT", Name ="Federal Capital Territory" },
                new State { Code="GM", Name ="Gombe" },
                new State { Code="IM", Name ="Imo" },
                new State { Code="JG", Name ="Jigawa" },
                new State { Code="KB", Name ="Kebbi" },
                new State { Code="KD", Name ="Kaduna" },
                new State { Code="KG", Name ="Kogi" },
                new State { Code="KN", Name ="Kano" },
                new State { Code="KT", Name ="Katsina" },
                new State { Code="KW", Name ="Kwara" },
                new State { Code="LA", Name ="Lagos" },
                new State { Code="NG", Name ="Niger" },
                new State { Code="NR", Name ="Nassarawa" },
                new State { Code="OD", Name ="Ondo" },
                new State { Code="OG", Name ="Ogun" },
                new State { Code="OS", Name ="Osun" },
                new State { Code="OT", Name ="Non-Nigerian" },
                new State { Code="OY", Name ="Oyo" },
                new State { Code="PL", Name ="Plateau" },
                new State { Code="RV", Name ="Rivers" },
                new State { Code="SO", Name ="Sokoto" },
                new State { Code="TR", Name ="Taraba" },
                new State { Code="YB", Name ="Yobe" },
                new State { Code="ZF", Name ="Zamfara" }
            };

            context.States.AddRange(state);
            await context.SaveChangesAsync();
        }
        public static async Task SeedCountry(MarkRiderContext context) 
        {
            var country = new []{new Country{Name = "Nigeria"}};
            context.Countries.AddRange(country);
            await context.SaveChangesAsync();
        }
        public static async Task SeedDeliverycancelation(MarkRiderContext context)
        {
            var reasons = new[] { 
                new DeliveryCancelationReasons { DeliveryReason = "Parcel not as same as shown on the shipment" },
                new DeliveryCancelationReasons { DeliveryReason = "Oversized shipment " },
                new DeliveryCancelationReasons { DeliveryReason = "Overweight shipment" }
            };
            context.DeliveryCancelationReasons.AddRange(reasons);
            await context.SaveChangesAsync();
        }

        public static async Task SeedDelivery(MarkRiderContext context)
        {
            var delivery = new []
            {
                new Delivery{DeliveryNo  = "DEL-40392", Email = "test@markrider@gmail.com",TotalAmount  = 10000, DateCreated = DateTime.UtcNow}
            };
            context.Deliveries.AddRange(delivery);
             await context.SaveChangesAsync();
        }
        public static async Task SeedFIle(MarkRiderContext context)
        {
            var file = new []
            {
                new FileData{Name = "single delivery", URL = "images/products/drink.jpeg"}
            };
            await context.FileDatas.AddRangeAsync(file);
            await context.SaveChangesAsync();
        }
        
         public static async Task SeedDeliveryLocation(MarkRiderContext context)
        {
            var deliveryLocation = new []
            {
                new DeliveryLocation{BaseAddress ="No 23 Ajileye street shomolu bariga",TargetAddress ="No 7 Ikorudu road",XLogitude  = 6.586853, XLatitude = 3.180396,DeliveryDistance  = 10,YLatitude = 06.621230,YLogitude = 003.515945,DateCreated = DateTime.UtcNow}
                
            };
            context.DeliveryLocations.AddRange(deliveryLocation);
            await context.SaveChangesAsync();
        }
        public static async Task SeedDeliveryItems(MarkRiderContext context)
        {
            var deliveryItem = new [] 
            {
                new DeliveryItem{PickUpItems ="Test Data1",DeliveryAmount =5000,DeliveryDate = DateTime.UtcNow,DeliveryTpe = Core.Enum.DeliveryTpe.Single,DeliveryStatus = Core.Enum.DeliveryStatus.Processing,Carriers = Core.Enum.Carriers.Bikes,PickUpPhone ="09069594949",DropOffPhone = "09089786756",DateCreated = DateTime.UtcNow,ImageUrl ="Test.jpeg", DeliveryId =1,DeliveryLocationId = 1}
            };
            context.DeliveryItems.AddRange(deliveryItem);
             await context.SaveChangesAsync();       
        }
        public static async Task SeedAdditionalCharges(MarkRiderContext context)
        {
            var AdditionalCharges = new[] 
            {
                new AdditionalCharges(){PaymentMethod = PaymentMethod.Paystack, Name="Transaction Fee (PSTK)", IsFixed = false, Amount = 1000, Rate = 1, CreatedBy = 1, DateCreated = DateTime.Now}
            };
            context.AdditionalCharges.AddRange(AdditionalCharges);
            await context.SaveChangesAsync();
        }

        public static async Task SeedDeliveryDistance(MarkRiderContext context)
        {
            var deliveryDistance = new []
            {
                new DeliveryDistance{Distance = 20, Amount = 1000},
                new DeliveryDistance{Distance = 30, Amount = 1200},
                new DeliveryDistance{Distance = 35, Amount = 1500},
                new DeliveryDistance{Distance = 45, Amount = 2000},
                new DeliveryDistance{Distance = 60, Amount = 2500},
                new DeliveryDistance{Distance = 65, Amount = 3000},
                new DeliveryDistance{Distance = 70, Amount = 4000}
            };
            context.DeliveryDistances.AddRange(deliveryDistance);
             await context.SaveChangesAsync();
        }
    }
    
}