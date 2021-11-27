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
                new State { Id="AB", Name ="Abia" },
                new State { Id="AD", Name ="Adamawa" },
                new State { Id="AK", Name ="Akwa Ibom" },
                new State { Id="AN", Name ="Anambra" },
                new State { Id="BA", Name ="Bauchi" },
                new State { Id="BE", Name ="Benue" },
                new State { Id="BO", Name ="Borno" },
                new State { Id="BY", Name ="Bayelsa" },
                new State { Id="CR", Name ="Cross Rive" },
                new State { Id="DT", Name ="Delta" },
                new State { Id="EB", Name ="Ebonyi" },
                new State { Id="ED", Name ="Edo" },
                new State { Id="EN", Name ="Enugu" },
                new State { Id="ET", Name ="Ekiti" },
                new State { Id="FCT", Name ="Federal Capital Territory" },
                new State { Id="GM", Name ="Gombe" },
                new State { Id="IM", Name ="Imo" },
                new State { Id="JG", Name ="Jigawa" },
                new State { Id="KB", Name ="Kebbi" },
                new State { Id="KD", Name ="Kaduna" },
                new State { Id="KG", Name ="Kogi" },
                new State { Id="KN", Name ="Kano" },
                new State { Id="KT", Name ="Katsina" },
                new State { Id="KW", Name ="Kwara" },
                new State { Id="LA", Name ="Lagos" },
                new State { Id="NG", Name ="Niger" },
                new State { Id="NR", Name ="Nassarawa" },
                new State { Id="OD", Name ="Ondo" },
                new State { Id="OG", Name ="Ogun" },
                new State { Id="OS", Name ="Osun" },
                new State { Id="OT", Name ="Non-Nigerian" },
                new State { Id="OY", Name ="Oyo" },
                new State { Id="PL", Name ="Plateau" },
                new State { Id="RV", Name ="Rivers" },
                new State { Id="SO", Name ="Sokoto" },
                new State { Id="TR", Name ="Taraba" },
                new State { Id="YB", Name ="Yobe" },
                new State { Id="ZF", Name ="Zamfara" }
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
                new DeliveryItem{PickUpItems ="Test Data1",DeliveryAmount =5000,DeliveryDate = DateTime.UtcNow,DeliveryTpe = Core.Enum.DeliveryTpe.Single,DeliveryStatus = Core.Enum.DeliveryStatus.Processing,Carriers = Core.Enum.Carriers.Bikes,PickUpPhone ="09069594949",DropOffPhone = "09089786756",DateCreated = DateTime.UtcNow,FileDataId =1, DeliveryId =1,DeliveryLocationId = 1}
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