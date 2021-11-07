using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Enum;
using Microsoft.Extensions.Logging;

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
                if(!context.AppUsers.Any())
                   await SeedUser(context);
                if(!context.Deliveries.Any())
                   await SeedDelivery(context);
                if(!context.DeliveryLocations.Any())
                   await SeedDeliveryLocation(context);
                if(!context.FileDatas.Any())
                    await SeedFIle(context);
               if(!context.DeliveryItems.Any())
                   await SeedDeliveryItems(context);
                if(!context.DeliveryDistances.Any())
                   await SeedDeliveryDistance(context);
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

        public static async Task SeedUser(MarkRiderContext context)
        {
            var user = new []
            {
                new AppUser
                {
                    UserName ="test@markrider@gmail.com",
                    PasswordHash = Encoding.ASCII.GetBytes("p@55word"),
                    PasswordSalt = Encoding.ASCII.GetBytes("p@55word"),
                    FirstName  = "Test",
                    LastName  ="Test",
                    Address  = "Test ",
                    Avatar  = "Tets",
                    IsActive  = true,
                    UserTypes = UserTypes.Users,
                    DateRegistered = DateTime.UtcNow,
                    Gender  = Gender.Male,
                    StateId = "AB",
                    CountryId = 1
                }
            };
            context.AppUsers.AddRange(user);
            await context.SaveChangesAsync();
        }
        public static async Task SeedDelivery(MarkRiderContext context)
        {
            var delivery = new []
            {
                new Delivery{DeliveryNo  = "DEL-40392", AppUserId = 1,TotalAmount  = 10000, DateCreated = DateTime.UtcNow}
            };
            context.Deliveries.AddRange(delivery);
             await context.SaveChangesAsync();
        }

        public static async Task SeedDeliveryLocation(MarkRiderContext context)
        {
            var deliveryLocation = new []
            {
                new DeliveryLocation{Address ="No 23 Ajileye street shomolu bariga",Logitude  = 2043038292, Latitude = 48929920, DeliveryDistance  = 10,DateCreated = DateTime.UtcNow}
                
            };
            context.DeliveryLocations.AddRange(deliveryLocation);
             await context.SaveChangesAsync();
        }
        public static async Task SeedFIle(MarkRiderContext context)
        {
            var file = new []
            {
                new FileData{Name = "single delivery", URL = "drink.jpeg"}
            };
            await context.FileDatas.AddRangeAsync(file);
            await context.SaveChangesAsync();
        }
        public static async Task SeedDeliveryItems(MarkRiderContext context)
        {
            var deliveryItem = new [] 
            {
                new DeliveryItem{PickUpItems ="Test Data1",DeliveryAmount =5000,DeliveryDate = DateTime.UtcNow,DeliveryTpe = Core.Enum.DeliveryTpe.Single,DeliveryStatus = Core.Enum.DeliveryStatus.Processing,Carriers = Core.Enum.Carriers.Bikes,PickUpPhone ="09069594949",DropOffPhone = "09089786756",DateCreated = DateTime.UtcNow,DeliveryId =1,FileDataId =1,DeliveryLocationId =1}
            };
            context.DeliveryItems.AddRange(deliveryItem);
             await context.SaveChangesAsync();       
        }
        public static async Task SeedDeliveryDistance(MarkRiderContext context)
        {
            var deliveryDistance = new []
            {
                new DeliveryDistance{Distance = 15, Amount = 1000},
                new DeliveryDistance{Distance =25, Amount = 1500},
                new DeliveryDistance{Distance = 30, Amount = 2000},
                new DeliveryDistance{Distance = 40, Amount = 2500},
                new DeliveryDistance{Distance = 50, Amount = 3000},
                new DeliveryDistance{Distance = 60, Amount = 3500},
                new DeliveryDistance{Distance = 61, Amount = 5000}
            };
            context.DeliveryDistances.AddRange(deliveryDistance);
             await context.SaveChangesAsync();
        }
    }
    
}