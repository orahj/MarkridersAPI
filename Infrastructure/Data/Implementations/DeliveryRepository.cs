using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs;
using Core.DTOs.Delivery;
using Core.DTOs.Payment;
using Core.Entities;
using Core.Entities.Identity;
using Core.Enum;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Implementations
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly ISecurityService _security;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentRepository _paymentRepository;
        private readonly UserManager<AppUser> _userManager;

        public DeliveryRepository(ISecurityService security, IUnitOfWork unitOfWork, IPaymentRepository paymentRepository, UserManager<AppUser> userManager)
        {
            _paymentRepository = paymentRepository;
            _unitOfWork = unitOfWork;
            _security = security;
            _userManager = userManager;
        }

        public async Task<Delivery> CreateDeliveryAsync(DeliveryDTO model)
        {
            //check its not a rider
            //var rider = await _unitOfWork.Repository<Rider>().GetByIdAsync()
             //generate transaction ref
           var transactinref = _security.GetCode("DL").ToUpper();
           //create Delivery
           var delivery = new Delivery(transactinref,model.Email);
            _unitOfWork.Repository<Delivery>().Add(delivery);
            //save delivery to get Id
            await _unitOfWork.Complete();

            List<DeliveryItem> items = new List<DeliveryItem>();
            List<DeliveryLocation> locations = new List<DeliveryLocation>();
            if(model.DeliveryItems.Count > 0)
            {
                foreach (var item in model.DeliveryItems)
                {
                    //chec delivery type of single
                    if(item.DeliveryTpe == DeliveryTpe.Single)
                    {
                        if(model.DeliveryItems.Count > 1)
                        {
                            int i = 0;
                            item.BaseLocation = model.DeliveryItems[i].BaseLocation;
                            item.TargetLocation = model.DeliveryItems[i].TargetLocation;
                            item.DeliveryTime = model.DeliveryItems[i].DeliveryTime;
                            item.Carriers = model.DeliveryItems[i].Carriers;
                            item.PickUpPhone = model.DeliveryItems[i].PickUpPhone;
                            item.DropOffPhone = model.DeliveryItems[i].DropOffPhone;
                        }
                    }
                    if(item.DeliveryTpe == DeliveryTpe.BulkDelivery)
                    {

                        int i = 0;
                        if (model.DeliveryItems.Count > 1)
                        {
                            item.BaseLocation = model.DeliveryItems[i].BaseLocation;
                            item.DeliveryTime = model.DeliveryItems[i].DeliveryTime;
                            item.Carriers = model.DeliveryItems[i].Carriers;
                            item.PickUpPhone = model.DeliveryItems[i].PickUpPhone;
                            item.DropOffPhone = model.DeliveryItems[i].DropOffPhone;
                        }
                        if(item.TargetLocation == null)
                        {
                            item.TargetLocation = model.DeliveryItems[i].TargetLocation;
                        }
                    }
                      //get distance covered
                    double distanceToCover = CalculateDistance(item.BaseLocation,item.TargetLocation);
                    //get distance amount
                   
                    var amounts = await _unitOfWork.Repository<DeliveryDistance>().ListAllAsync();
                    //TODO
                    //base fair 300 naira for bikes
                    //100 naira per kilometer plus base fair
                    //amount = kilometer * 100 + 300
                     var amt = new DeliveryDistance();
                    if(distanceToCover <= 20)
                    {
                        amt = amounts.Where(x => x.Distance == 20).FirstOrDefault();
                    }
                    else if(distanceToCover > 20 && distanceToCover <= 30)
                    {
                        amt = amounts.Where(x => x.Distance == 30).FirstOrDefault();
                    }
                    else if(distanceToCover > 30 && distanceToCover <= 35)
                    {
                        amt = amounts.Where(x => x.Distance == 35).FirstOrDefault();
                    }
                    else if(distanceToCover > 35 && distanceToCover <= 45)
                    {
                        amt = amounts.Where(x => x.Distance == 45).FirstOrDefault();
                    }
                    else if(distanceToCover > 45 && distanceToCover <= 60 )
                    {
                        amt = amounts.Where(x => x.Distance == 60).FirstOrDefault();
                    }
                    else if(distanceToCover > 60 && distanceToCover <= 65 )
                    {
                        amt = amounts.Where(x => x.Distance == 65).FirstOrDefault();
                    }
                    else
                    {
                        amt = amounts.Where(x => x.Distance == 70).FirstOrDefault();
                    }
                    if(item.DeliveryTime == Core.Enum.DeliveryTime.RigthAway)
                    {
                        item.ScheduledDeliveryDate = DateTimeOffset.Now;
                    }
                    if(amt.Amount > 0)
                    {
                        var location = new DeliveryLocation(item.BaseLocation.Address,
                        item.BaseLocation.Longitude,item.BaseLocation.Latitude,distanceToCover,item.TargetLocation.Longitude,
                        item.TargetLocation.Latitude,item.TargetLocation.Address);
                        _unitOfWork.Repository<DeliveryLocation>().Add(location);
                        await _unitOfWork.Complete();

                        var deliveryItem = new DeliveryItem(item.PickUpItems,amt.Amount,item.DeliveryTpe,
                        item.DeliveryTime,item.Carriers,
                        item.PickUpPhone,item.DropOffPhone,item.ImageUrl,delivery.Id,location.Id,item.ScheduledDeliveryDate);
                        items.Add(deliveryItem);
                        _unitOfWork.Repository<DeliveryItem>().Add(deliveryItem);
                        await _unitOfWork.Complete();
                    }

                }
            }
            //get total amount

            var total = items.Sum(x => x.DeliveryAmount);
            //save Transaction
            var amountWithCharge = await _paymentRepository.GetPaymentCharges(total);
            Transaction tran = new Transaction(total, amountWithCharge.Total);
            _unitOfWork.Repository<Transaction>().Add(tran);

            //save changes to context
            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;

            //Get total
            var updateDel = await _unitOfWork.Repository<Delivery>().GetByIdAsync(delivery.Id);
            updateDel.transactionId = tran.Id;
            updateDel.TotalAmount = total;
            _unitOfWork.Repository<Delivery>().Update(updateDel);
            await _unitOfWork.Complete();
            //update delivery amount

            //Save Delivery locations 
            foreach (var item in locations)
            {
                 _unitOfWork.Repository<DeliveryLocation>().Add(item);
            }
           
            updateDel.DeliveryItems = items;
            //add notification record
            var userEmail = await _userManager.FindByEmailAsync(model.Email);
            if (userEmail != null)
            {
                var notification = new Notification
                {
                    AppUserId = userEmail.Id.ToString(),
                    DateCreated = DateTime.Now,
                    Read = false,
                    Type = NotificationType.DeliveryUpdate,
                    Data = new Dictionary<string, string>
                    {
                        { "Title", $"New Delivery: {updateDel.DeliveryNo}" },
                        { "Body", $"You just created a new delivery, on {DateTime.Now}. Delivery amount is {updateDel.TotalAmount}." },
                        { "DeliveryId", $"{updateDel.Id}"}
                    }
                };
                _unitOfWork.Repository<Notification>().Add(notification);
                await _unitOfWork.Complete();
            }

            return updateDel;

        }

        public async Task<int> GetCountAsync(string sort, string email, SpecParams specParams)
        {
             var countSpec = new DeliveryWithFilterCountSpec(sort, email, specParams);
            return await  _unitOfWork.Repository<Delivery>().CountAsync(countSpec);
        }

        public async Task<IReadOnlyList<DeliveryItem>> GetDeliverItemsyAsync()
        {
            var spec = new DeliveryItemSpecification();
            return await _unitOfWork.Repository<DeliveryItem>().ListAsync(spec);
        }

        public async Task<IReadOnlyList<Delivery>> GetDeliveryAsync()
        {
          return await _unitOfWork.Repository<Delivery>().ListAllAsync();
        }

        public async Task<IReadOnlyList<Delivery>> GetDeliveryByEmailAsync(string email)
        {
            var spec = new DeliverySpecification(email);
             return await _unitOfWork.Repository<Delivery>().ListAsync(spec);
        }
        public async Task<Delivery> GetDeliveryByDeliveryNoAsync(string email,string shipmentNo)
        {
            var spec = new DeliverySpecification(email,shipmentNo);
             return await _unitOfWork.Repository<Delivery>().GetEntityWithSpec(spec);
        }

        public async Task<Delivery> GetDeliveryByIdAsync(int Id)
        {
             var spec = new DeliverySpecification(Id);
             return await _unitOfWork.Repository<Delivery>().GetEntityWithSpec(spec);
        }

        public async Task<IReadOnlyList<DeliveryDistance>> GetDeliveryDistanceAsync()
        {
            return await _unitOfWork.Repository<DeliveryDistance>().ListAllAsync();
        }

        public async Task<DeliveryDistance> GetDeliveryDistanceByIdAsync(int Id)
        {
            var spec = new DeliveryDistanceSpec(Id);
            return await _unitOfWork.Repository<DeliveryDistance>().GetEntityWithSpec(spec);
        }

        public async Task<IReadOnlyList<Delivery>> GetDeliveryForUserAsync(string sort, string email, SpecParams specParams)
        {
            var spec = new DeliverySpecification(sort, email, specParams);
            return await _unitOfWork.Repository<Delivery>().ListAsync(spec);
        }
        
        public async Task<DeliveryItem> GetDeliveryItemByIdAsync(int Id)
        {
            var spec = new DeliveryItemSpecification(Id);
            return await _unitOfWork.Repository<DeliveryItem>().GetEntityWithSpec(spec);
        }

        public async Task<DeliveryLocation> GetDeliveryLocationByIdAsync(int Id)
        {
            var spec = new DeliveryLocationeSpec(Id);
            return await _unitOfWork.Repository<DeliveryLocation>().GetEntityWithSpec(spec);
        }

        public async Task<IReadOnlyList<DeliveryLocation>> GetDeliveryLocationsAsync()
        {
            var spec = new DeliveryLocationeSpec();
            return await _unitOfWork.Repository<DeliveryLocation>().ListAsync(spec);
        }
         private double CalculateDistance(DeliveryLocationDTO point1, DeliveryLocationDTO point2)
        {
            var d1 = point1.Latitude * (Math.PI / 180.0);
            var num1 = point1.Longitude * (Math.PI / 180.0);
            var d2 = point2.Latitude * (Math.PI / 180.0);
            var num2 = point2.Longitude * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) +
                    Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);
            double distanceInMeter =  6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
            return distanceInMeter/1000;
        }
       
        public async Task<Transaction> GetDeliveryTransactionByIdAsync(int Id)
        {
            var spec = new TransactionWithSPec(Id);
            return await _unitOfWork.Repository<Transaction>().GetEntityWithSpec(spec);
        }

        public async Task<DeliveryItem> GetDeliveryItemByDeliveryIdAsync(int Id, int deliveryId)
        {
            var spec = new DeliveryItemSpecification(Id,deliveryId);
            return await _unitOfWork.Repository<DeliveryItem>().GetEntityWithSpec(spec);
        }
        public async Task<IReadOnlyList<DeliveryItem>> GetDeliverItemsybyDeliveryAsync(int Id)
        {
            var spec = new DeliveryItemSpecification(Id, true);
            return await _unitOfWork.Repository<DeliveryItem>().ListAsync(spec);
        }

        public async Task<IReadOnlyList<DeliveryCancelationReasons>> GetCancelationReasonsAsync()
        {
           return await _unitOfWork.Repository<DeliveryCancelationReasons>().ListAllAsync();
        }
    }
}