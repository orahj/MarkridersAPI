using Core.DTOs;
using Core.DTOs.Delivery;
using Core.Entities;
using Core.Entities.Identity;
using Core.Enum;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Implementations
{
    public class DeliveryDetailsRepository : IDeliveryDetailsRepository
    {
        private readonly ISecurityService _security;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly MarkRiderContext _context;

        public DeliveryDetailsRepository(ISecurityService security, IUnitOfWork unitOfWork, UserManager<AppUser> userManager, MarkRiderContext context)
        {
            _unitOfWork = unitOfWork;
            _security = security;
            _userManager = userManager;
            _context = context;
        }
        public async Task<Result> CancelDeliveryAsync(DeliveryDetailDTO model)
        {
            if (model == null) return  new Result { IsSuccessful = false, Message="Object can not be null"};
            // get delivery details
            //check if its a rider
            var riderSpec = new RiderSpec(model.AppUserId);
            var rider = await _unitOfWork.Repository<Rider>().GetEntityWithSpec(riderSpec);
            if(rider == null)
            {
                return new Result { IsSuccessful = false, Message = "Only a rider can perform this operation" };
            }
            var spec = new DeliveryDetailsSpecification(model.DeliveriesId);
            var del = await _unitOfWork.Repository<DeliveryDetails>().GetEntityWithSpec(spec);
            if (del == null) return new Result { IsSuccessful = false, Message = "Delivery not asigned to this rider" };
            
            //get delivery
            var deliverySpec = new DeliverySpecification(del.DeliveriesId);
            var delivery = await _unitOfWork.Repository<Delivery>().GetEntityWithSpec(deliverySpec);
            if (delivery != null)
            {
                //update delivery status to canceled
                foreach (var item in delivery.DeliveryItems)
                {
                    var deliveryitem = await _unitOfWork.Repository<DeliveryItem>().GetByIdAsync(item.Id);
                    deliveryitem.DeliveryStatus = Core.Enum.DeliveryStatus.Canceled;
                    _unitOfWork.Repository<DeliveryItem>().Update(deliveryitem);
                    await _unitOfWork.Complete();
                }

                //get user
                var user = await _userManager.FindByEmailAsync(delivery.Email);
                //refun wallet 
                var specWallet = new WalletSpec(user.Id.ToString());
                var wallet = await _unitOfWork.Repository<Wallet>().GetEntityWithSpec(specWallet);
                if (wallet != null)
                {
                    //update wallet amount
                    var walletBalance = wallet.Balance + delivery.TotalAmount;
                    wallet.Balance = walletBalance;
                    _unitOfWork.Repository<Wallet>().Update(wallet);
                    //Create wallet tranaction
                    //generate transaction ref
                    var transactinref = _security.GetCode("WAL").ToUpper();
                    WalletTransaction transaction = new WalletTransaction(wallet.Id, Core.Enum.TransactionType.WalletTopUp, delivery.TotalAmount,
                    "Deposit", transactinref, Core.Enum.WalletTransactionStatus.Successful);
                    _unitOfWork.Repository<WalletTransaction>().Add(transaction);

                    //cancel delivery
                    del.IsCanceled = true;
                    del.CancelReason = model.Reason;
                    del.Canceleduser = rider.AppUserId;
                    del.Deliverystatus = "Canceled";
                    _unitOfWork.Repository<DeliveryDetails>().Update(del);

                    //save changes to context
                    await _unitOfWork.Complete();
                }
                var userEmail = await _userManager.FindByEmailAsync(delivery.Email);
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
                        { "Title", $"Canceled Delivery By: {userEmail.Email}" },
                        { "Body", $"You just cancled this delivery, on {DateTime.Now}. Delivery Number is {delivery.DeliveryNo}." },
                        { "DeliveryId", $"{delivery.Id}"}
                    }
                    };
                    _unitOfWork.Repository<Notification>().Add(notification);
                    await _unitOfWork.Complete();
                }
            }
            return new Result { IsSuccessful = true, Message = "Delivery Canceled successfully!" };
        }

        public async Task<Result> AsignDeliveryAsync(AsigndeliveryDTO model)
        {
            if (model == null) return new Result { IsSuccessful = false, Message = "Object can not be null" };
            //check if rider
            var riderspec = new RiderSpec(model.AppUserId);
            var rider = await _unitOfWork.Repository<Rider>().GetEntityWithSpec(riderspec);
            if(rider == null)
            {
                return new Result { IsSuccessful = false, Message = "Rider not found" };
            }

            //get deliveryitem
            var spec = new DeliverySpecification(model.DeliveriesId);
            var deliverydetails = await _unitOfWork.Repository<Delivery>().GetEntityWithSpec(spec);
            if (deliverydetails.IscancledByUser)
            {
                return new Result { IsSuccessful = false, Message = "Delivery has been canceled by user" };
            }
            //check if delivery has be assigned
            var deldetailsSpec = new DeliveryDetailsSpecification(deliverydetails.Id);
            var asingeddelivery = await _unitOfWork.Repository<DeliveryDetails>().GetEntityWithSpec(deldetailsSpec);
            if(asingeddelivery != null)
            {
                return new Result { IsSuccessful = false, Message = "Delivery has ben asigned already!" };
            }
            //update delivery status to asigned
            foreach (var item in deliverydetails.DeliveryItems)
            {
                var deliveryitem = await _unitOfWork.Repository<DeliveryItem>().GetByIdAsync(item.Id);
                deliveryitem.DeliveryStatus = Core.Enum.DeliveryStatus.Assigned;
                _unitOfWork.Repository<DeliveryItem>().Update(deliveryitem);
                await _unitOfWork.Complete();
            }
            var delivery = new DeliveryDetails(model.AppUserId, model.DeliveriesId,"Processing",deliverydetails.TotalAmount);
            _unitOfWork.Repository<DeliveryDetails>().Add(delivery);
            //save delivery to get Id
            await _unitOfWork.Complete();

            //User notification
            var userEmail = await _userManager.FindByEmailAsync(deliverydetails.Email);
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
                        { "Title", $"Delivery Assigned: {deliverydetails.DeliveryNo}" },
                        { "Body", $"Your delivery has been assigned, on {DateTime.Now}. Delivery Number is {deliverydetails.DeliveryNo}." },
                        { "DeliveryId", $"{delivery.Id}"}
                    }
                };
                _unitOfWork.Repository<Notification>().Add(notification);
                await _unitOfWork.Complete();
            }
            //rider notification 
            var riderEmail = await _userManager.FindByIdAsync(model.AppUserId);
            if (riderEmail != null)
            {
                var notification = new Notification
                {
                    AppUserId = riderEmail.Id.ToString(),
                    DateCreated = DateTime.Now,
                    Read = false,
                    Type = NotificationType.DeliveryUpdate,
                    Data = new Dictionary<string, string>
                    {
                        { "Title", $"New delivery assigned: {deliverydetails.DeliveryNo}" },
                        { "Body", $"You have been assigned a delivery, on {DateTime.Now}. Delivery Number is {deliverydetails.DeliveryNo}." },
                        { "DeliveryId", $"{delivery.Id}"}
                    }
                };
                _unitOfWork.Repository<Notification>().Add(notification);
                await _unitOfWork.Complete();
            }
            return new Result { IsSuccessful = true, Message = "Delivery Asigned successfully!" };
        }

        public async Task<Result> GetDeliveryDetailsByEmailAsync(string userId)
        {
            var delDetails = new List<DeliveryDetails>();
            var spec = new DeliveryDetailsSpecification(userId);
            //var deliveries = await _unitOfWork.Repository<DeliveryDetails>().ListAsync(spec);
            var riderdeliveries = await _context.DeliveryDetails.Include(x=>x.Deliveries).Where(x => x.AppUserId == userId)
                .OrderByDescending(x => x.Id)
                .ToListAsync();
            var ratings = new Ratings();
            if(riderdeliveries != null)
            {
                foreach(var item in riderdeliveries)
                {
                    if(item.RatingId != null)
                    {
                        ratings = await _unitOfWork.Repository<Ratings>().GetByIdAsync(item.RatingId.GetValueOrDefault());
                    }
                    item.Ratings = ratings;
                    delDetails.Add(item);
                }
            }
            return new Result { IsSuccessful = true, Message = "deliveries retireved", ReturnedObject = delDetails };
        }

        public async Task<Result> CancelDeliveryByUserAsync(DeliveryDetailDTO model)
        {
            if (model == null) return new Result { IsSuccessful = false, Message = "Object can not be null" };
            //get delivery
            var spec = new DeliverySpecification(model.DeliveriesId);
            var delivery = await _unitOfWork.Repository<Delivery>().GetEntityWithSpec(spec);
            if(delivery == null)
            {
                return new Result { IsSuccessful = false, Message = "delivery not found!" };
            }
            if (delivery != null)
            {
                //check if delivery has be assigned
                var deldetailsSpec = new DeliveryDetailsSpecification(delivery.Id);
                var deliverydetails = await _unitOfWork.Repository<DeliveryDetails>().GetEntityWithSpec(deldetailsSpec);
                if(deliverydetails != null) 
                {
                    return new Result { IsSuccessful = false, Message = "Delivery has been assigned!" };
                }

                //update delivery status to asigned
                foreach (var item in delivery.DeliveryItems)
                {
                    var deliveryitem = await _unitOfWork.Repository<DeliveryItem>().GetByIdAsync(item.Id);
                    deliveryitem.DeliveryStatus = Core.Enum.DeliveryStatus.Canceled;
                    _unitOfWork.Repository<DeliveryItem>().Update(deliveryitem);
                    await _unitOfWork.Complete();
                }
                //get user
                var user = await _userManager.FindByEmailAsync(delivery.Email);
                //refun wallet 
                var specWallet = new WalletSpec(user.Id.ToString());
                var wallet = await _unitOfWork.Repository<Wallet>().GetEntityWithSpec(specWallet);
                if (wallet != null)
                {
                    //update wallet amount
                    var walletBalance = wallet.Balance + delivery.TotalAmount;
                    wallet.Balance = walletBalance;
                    _unitOfWork.Repository<Wallet>().Update(wallet);
                    //Create wallet tranaction
                    //generate transaction ref
                    var transactinref = _security.GetCode("WAL").ToUpper();
                    WalletTransaction transaction = new WalletTransaction(wallet.Id, Core.Enum.TransactionType.WalletTopUp, delivery.TotalAmount,
                    "Deposit", transactinref, Core.Enum.WalletTransactionStatus.Successful);
                    _unitOfWork.Repository<WalletTransaction>().Add(transaction);

                    ////cancel delivery
                    delivery.IscancledByUser = true;
                    delivery.ReasonForCanling = model.Reason;
                    _unitOfWork.Repository<Delivery>().Update(delivery);
                    ////save changes to context
                    await _unitOfWork.Complete();
                    //notification
                    var userEmail = await _userManager.FindByEmailAsync(delivery.Email);
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
                                { "Title", $"Delivery Cancellation: {delivery.DeliveryNo}" },
                                { "Body", $"You just canceled this delivery, on {DateTime.Now}. Delivery Number is {delivery.DeliveryNo}." },
                                { "DeliveryId", $"{delivery.Id}"}
                            }
                        };
                        _unitOfWork.Repository<Notification>().Add(notification);
                        await _unitOfWork.Complete();
                    }
                }
            }

            return new Result { IsSuccessful = true, Message = "Delivery Canceled successfully!" };

        }
        public async Task<Result> CompletDeliveryAsync(DeliveryCompletionDTO model)
        {
            if (model == null) return new Result { IsSuccessful = false, Message = "Object can not be null" };
            var riderspec = new RiderSpec(model.AppUserId);
            var rider = await _unitOfWork.Repository<Rider>().GetEntityWithSpec(riderspec);
            if (rider != null)
            {
                return new Result { IsSuccessful = false, Message = "This action cannot be completed by a rider!" };
            }
            //get delivery
            var spec = new DeliverySpecification(model.DeliveriesId);
            var delivery = await _unitOfWork.Repository<Delivery>().GetEntityWithSpec(spec);
            if (delivery == null)
            {
                return new Result { IsSuccessful = false, Message = "delivery not found!" };
            }
            if(delivery.Email != model.Email)
            {
                return new Result { IsSuccessful = false, Message = "This delivery belongs to another user!" };
            }
            if (delivery != null)
            {
                //check if delivery has be assigned
                var deldetailsSpec = new DeliveryDetailsSpecification(delivery.Id);
                var deliverydetails = await _unitOfWork.Repository<DeliveryDetails>().GetEntityWithSpec(deldetailsSpec);
                if (deliverydetails == null)
                {
                    return new Result { IsSuccessful = false, Message = "Only asigned deliveries can be completed!" };
                }
                //delivery status
                if (deliverydetails.Deliverystatus == "Completed")
                {
                    return new Result { IsSuccessful = false, Message = "This delivery is completed!" };
                }
                if (deliverydetails.Deliverystatus == "Started" || deliverydetails.Deliverystatus == "Processing")
                {
                    return new Result { IsSuccessful = false, Message = "Delivery in progress!" };
                }
                //update delivery status to asigned
                foreach (var item in delivery.DeliveryItems)
                {
                    var deliveryitem = await _unitOfWork.Repository<DeliveryItem>().GetByIdAsync(item.Id);
                    deliveryitem.DeliveryStatus = Core.Enum.DeliveryStatus.Completed;
                    _unitOfWork.Repository<DeliveryItem>().Update(deliveryitem);
                    await _unitOfWork.Complete();
                }
                //check rating
                var ratings = new Ratings();
                if (model.Rating > 0)
                {
                    ratings = new Ratings(model.Rating.GetValueOrDefault(), model.Ratingcomment, deliverydetails.AppUserId);
                    _unitOfWork.Repository<Ratings>().Add(ratings);
                    await _unitOfWork.Complete();
                }
                //deliveryCompleted
                deliverydetails.IsCompleted = true;
                deliverydetails.Deliverystatus = "Completed";
                deliverydetails.RatingId = ratings != null ? ratings.Id : null;
                _unitOfWork.Repository<DeliveryDetails>().Update(deliverydetails);
                await _unitOfWork.Complete();

                //notification
                var userEmail = await _userManager.FindByEmailAsync(delivery.Email);
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
                                { "Title", $"Delivery Completion: {delivery.DeliveryNo}" },
                                { "Body", $"You just completed this delivery, on {DateTime.Now}. Delivery Number is {delivery.DeliveryNo}." },
                                { "DeliveryId", $"{delivery.Id}"}
                            }
                    };
                    _unitOfWork.Repository<Notification>().Add(notification);
                    await _unitOfWork.Complete();
                }
            }

            return new Result { IsSuccessful = true, Message = "Delivery Completed successfully!" };

        }
        public async Task<Result> DisputedDeliveryAsync(DeliveryDisputedDTO model)
        {
            if (model == null) return new Result { IsSuccessful = false, Message = "Object can not be null" };
            //get delivery
            var spec = new DeliverySpecification(model.DeliveriesId);
            var delivery = await _unitOfWork.Repository<Delivery>().GetEntityWithSpec(spec);
            if (delivery == null)
            {
                return new Result { IsSuccessful = false, Message = "delivery not found!" };
            }
            if (delivery != null)
            {
                //check if delivery has be assigned
                var deldetailsSpec = new DeliveryDetailsSpecification(delivery.Id);
                var deliverydetails = await _unitOfWork.Repository<DeliveryDetails>().GetEntityWithSpec(deldetailsSpec);
                if (deliverydetails == null)
                {
                    return new Result { IsSuccessful = false, Message = "Only asigned deliveries can be cancelled!" };
                }
                
                //delivery status
                if(deliverydetails.Deliverystatus =="Started" || deliverydetails.Deliverystatus == "Processing")
                {
                    return new Result { IsSuccessful = false, Message = "Delivery in progress!" };
                }
               
                //update delivery status to asigned
                foreach (var item in delivery.DeliveryItems)
                {
                    var deliveryitem = await _unitOfWork.Repository<DeliveryItem>().GetByIdAsync(item.Id);
                    deliveryitem.DeliveryStatus = Core.Enum.DeliveryStatus.Disputed;
                    _unitOfWork.Repository<DeliveryItem>().Update(deliveryitem);
                    await _unitOfWork.Complete();
                }
                //check rating
                if (model.Rating > 0)
                {
                    var ratings = new Ratings(model.Rating.GetValueOrDefault(), model.Ratingcomment, deliverydetails.AppUserId);
                    _unitOfWork.Repository<Ratings>().Add(ratings);
                    await _unitOfWork.Complete();
                }
                //dispued reason
                deliverydetails.DisputedComment = model.DisputdeReason;
                deliverydetails.IsDisputed = true;
                deliverydetails.DisputedUser = delivery.Email;
                deliverydetails.Deliverystatus = "Disputed";
                _unitOfWork.Repository<DeliveryDetails>().Update(deliverydetails);
                await _unitOfWork.Complete();

                //notification
                var userEmail = await _userManager.FindByEmailAsync(delivery.Email);
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
                                { "Title", $"Delivery Dispute: {delivery.DeliveryNo}" },
                                { "Body", $"You just disputed this delivery, on {DateTime.Now}. Delivery Number is {delivery.DeliveryNo}." },
                                { "DeliveryId", $"{delivery.Id}"}
                            }
                    };
                    _unitOfWork.Repository<Notification>().Add(notification);
                    await _unitOfWork.Complete();
                }
            }

            return new Result { IsSuccessful = true, Message = "Dispute submitted!" };

        }
        public async Task<Result> FulfilledDeliveryAsync(DeliverydeliveredDTO model)
        {
            if (model == null) return new Result { IsSuccessful = false, Message = "Object can not be null" };
            //check if its a rider
            //get delivery
            var spec = new DeliverySpecification(model.DeliveriesId);
            var delivery = await _unitOfWork.Repository<Delivery>().GetEntityWithSpec(spec);
            if (delivery == null)
            {
                return new Result { IsSuccessful = false, Message = "delivery not found!" };
            }
            if (delivery != null)
            {
                //check if delivery has be assigned
                var deldetailsSpec = new DeliveryDetailsSpecification(delivery.Id);
                var deliverydetails = await _unitOfWork.Repository<DeliveryDetails>().GetEntityWithSpec(deldetailsSpec);
                if (deliverydetails == null)
                {
                    return new Result { IsSuccessful = false, Message = "Only asigned deliveries can be ended!" };
                }
                //delivery status
                if (deliverydetails.Deliverystatus != "Started" || deliverydetails.Deliverystatus == "Processing" || deliverydetails.Deliverystatus == "Completed"
                    || deliverydetails.Deliverystatus =="Delivered" || deliverydetails.Deliverystatus == "Disputed")
                {
                    return new Result { IsSuccessful = false, Message = "Delivery in progress already!" };
                }
                //update delivery status to asigned
                foreach (var item in delivery.DeliveryItems)
                {
                    var deliveryitem = await _unitOfWork.Repository<DeliveryItem>().GetByIdAsync(item.Id);
                    deliveryitem.DeliveryStatus = Core.Enum.DeliveryStatus.Delivered;
                    _unitOfWork.Repository<DeliveryItem>().Update(deliveryitem);
                    await _unitOfWork.Complete();
                }
                deliverydetails.Deliverystatus = "Delivered";
                deliverydetails.IsCompleted = true;
                _unitOfWork.Repository<DeliveryDetails>().Update(deliverydetails);
                await _unitOfWork.Complete();

                //notification
                var userEmail = await _userManager.FindByEmailAsync(delivery.Email);
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
                                { "Title", $"Package Delivered: {delivery.DeliveryNo}" },
                                { "Body", $"Your package has been delivered on {DateTime.Now}. Delivery Number is {delivery.DeliveryNo}." },
                                { "DeliveryId", $"{delivery.Id}"}
                            }
                    };
                    _unitOfWork.Repository<Notification>().Add(notification);
                    await _unitOfWork.Complete();
                }

            }

            return new Result { IsSuccessful = true, Message = "Delivery fulfilled!" };

        }
        public async Task<Result> StartDeliveryAsync(DeliverydeliveredDTO model)
        {
            if (model == null) return new Result { IsSuccessful = false, Message = "Object can not be null" };
            //check if its a rider
            //get delivery
            var spec = new DeliverySpecification(model.DeliveriesId);
            var delivery = await _unitOfWork.Repository<Delivery>().GetEntityWithSpec(spec);
            if (delivery == null)
            {
                return new Result { IsSuccessful = false, Message = "delivery not found!" };
            }
            if (delivery != null)
            {
                //check if delivery has be assigned
                var deldetailsSpec = new DeliveryDetailsSpecification(delivery.Id);
                var deliverydetails = await _unitOfWork.Repository<DeliveryDetails>().GetEntityWithSpec(deldetailsSpec);
                if (deliverydetails == null)
                {
                    return new Result { IsSuccessful = false, Message = "Only asigned deliveries can be cancelled!" };
                }
                //delivery status
                if (deliverydetails.Deliverystatus != "Processing")
                {
                    return new Result { IsSuccessful = false, Message = "Delivery in progress already!" };
                }
                //update delivery status to asigned
                foreach (var item in delivery.DeliveryItems)
                {
                    var deliveryitem = await _unitOfWork.Repository<DeliveryItem>().GetByIdAsync(item.Id);
                    deliveryitem.DeliveryStatus = Core.Enum.DeliveryStatus.Started;
                    _unitOfWork.Repository<DeliveryItem>().Update(deliveryitem);
                    await _unitOfWork.Complete();
                }
                deliverydetails.Deliverystatus = "Started";
                _unitOfWork.Repository<DeliveryDetails>().Update(deliverydetails);
                await _unitOfWork.Complete();

                //notification
                var userEmail = await _userManager.FindByEmailAsync(delivery.Email);
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
                                { "Title", $"Delivery Started: {delivery.DeliveryNo}" },
                                { "Body", $"You just started this delivery, on {DateTime.Now}. Delivery Number is {delivery.DeliveryNo}." },
                                { "DeliveryId", $"{delivery.Id}"}
                            }
                    };
                    _unitOfWork.Repository<Notification>().Add(notification);
                    await _unitOfWork.Complete();
                }
            }

            return new Result { IsSuccessful = true, Message = "Delivery started!" };

        }
        public async Task<Result> RidersalesAsync(string userId)
        {
            var salesRecord = new RdiersalesDetailsDTO();
            var deliveries = await (from x in _context.DeliveryDetails
                                    where x.AppUserId == userId && x.IsCompleted
                                    orderby x.DateCreated
                                    select x)
                                    .Include(x => x.Deliveries).AsNoTracking().ToListAsync();
            var monthSales = deliveries.GroupBy(x => x.DateCreated.Month).Select(x => new { Month = x.Key, MonthlySales = x.Sum(a => a.DeliveryAmount) });
            foreach(var item in monthSales)
            {
                if(item.Month == DateTime.Now.Month)
                {
                    salesRecord.Monthlysales = item.MonthlySales;
                }
            }
            var totalSales = deliveries.Sum(x => x.DeliveryAmount);
            salesRecord.Totalsales = totalSales;
            var weeklySalesList = deliveries.Where(x => GetIso8601WeekOfYear(x.DateCreated.DateTime) == GetIso8601WeekOfYear(DateTime.Now));
            var weeklysales = weeklySalesList.Sum(x => x.DeliveryAmount);
            salesRecord.Weeklysales = weeklysales;
            return new Result { IsSuccessful = true, Message = "Successfully retrieved", ReturnedObject = salesRecord };
        }
        private int GetIso8601WeekOfYear(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public async Task<Result> TotalSalesAsync()
        {
            var salesRecord = new TotalsalessDTO();
            string highestUserSales = "";
            int highestCount = 0;
            var totalSales = await (from x in _context.DeliveryDetails where x.IsCompleted select x)
                .Include(x => x.Deliveries)
                .AsNoTracking().ToListAsync();
            var topRider = totalSales.GroupBy(x => x.AppUserId)
                .Select(x => new { UserId = x.Key, Sales = x.Count() });
            foreach(var item in topRider)
            {
                if(item.Sales > highestCount)
                {
                    highestCount = item.Sales;
                    var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == new Guid(item.UserId));
                    highestUserSales = user.Email;
                }
            }
            salesRecord.GrossRevenure = totalSales.Sum(x => x.DeliveryAmount);
            salesRecord.NetRevenure = totalSales.Sum(x => x.DeliveryAmount);
            salesRecord.deliveryCount = totalSales.Count();
            salesRecord.TopRider = highestUserSales;
            var deliveries = await _context.Deliveries.ToListAsync();
            return new Result { IsSuccessful = true, Message = "Record retrieved successfully", ReturnedObject = salesRecord };
        }

        public async Task<Result> GetDeliveryCountForRider()
        {
            var totalCount = new RiderSalesCount();
            var totalCountList = new List<RiderSalesCount>();
            var totalSales = await (from x in _context.DeliveryDetails where x.IsCompleted select x)
               .Include(x => x.Deliveries)
               .AsNoTracking().ToListAsync();
            var topRider = totalSales.GroupBy(x => x.AppUserId)
                .Select(x => new { UserId = x.Key, Sales = x.Count() });

            foreach (var item in topRider)
            {
                 var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == new Guid(item.UserId));
                totalCount = new RiderSalesCount
                {
                    DeliveryCount = item.Sales,
                    Email = user.Email,
                    UserId = user.Id.ToString()
                };
                totalCountList.Add(totalCount);
            }
            return new Result { IsSuccessful = true, ReturnedObject = totalCountList };
        }

        public async Task<Result> GetDeliveryAsignToRider(string id)
        {
            var riderDelveries = await  _context.DeliveryDetails.Where(x => x.AppUserId == id)
                .Include(x=>x.Deliveries)
                .ThenInclude(d=>d.DeliveryItems)
                .ThenInclude(l=>l.DeliveryLocation)
                .Include(x=>x.Ratings)
                .Include(x=>x.AppUser)
                .ToListAsync();
            return new Result { IsSuccessful = true, ReturnedObject = riderDelveries };
        }
    }
}
