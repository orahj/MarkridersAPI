using Core.DTOs;
using Core.DTOs.Delivery;
using Core.Entities;
using Core.Entities.Identity;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
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

        public DeliveryDetailsRepository(ISecurityService security, IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _security = security;
            _userManager = userManager;
        }
        public async Task<Result> CancelDeliveryAsync(DeliveryDetailDTO model)
        {
            if (model == null) return  new Result { IsSuccessful = false, Message="Object can not be null"};
            // get delivery details
            var spec = new DeliveryDetailsSpecification(model.AppUserId);
            var del = await _unitOfWork.Repository<DeliveryDetails>().GetEntityWithSpec(spec);
            //get delivery
            var delivery = await _unitOfWork.Repository<Delivery>().GetByIdAsync(model.DeliveriesId);
            if(delivery != null)
            {
                //get user
                var user = await _userManager.FindByEmailAsync(delivery.Email);
                //refun wallet 
                var specWallet = new WalletSpec(user.Id.ToString());
                var wallet = await _unitOfWork.Repository<Wallet>().GetEntityWithSpec(specWallet);
                if(wallet != null)
                {
                    //update wallet amount
                    var walletBalance = wallet.Balance + delivery.TotalAmount;
                    wallet.Balance = walletBalance;

                    //Create wallet tranaction
                    //generate transaction ref
                    var transactinref = _security.GetCode("WAL").ToUpper();
                    WalletTransaction transaction = new WalletTransaction(wallet.Id, Core.Enum.TransactionType.WalletTopUp, delivery.TotalAmount,
                    "Deposit", transactinref, Core.Enum.WalletTransactionStatus.Successful);
                    _unitOfWork.Repository<WalletTransaction>().Add(transaction);

                    //save changes to context
                     await _unitOfWork.Complete();
                }
            }
            _unitOfWork.Repository<DeliveryDetails>().Update(del);
            //save delivery to get Id
            await _unitOfWork.Complete();
            return new Result { IsSuccessful = true, Message = "Delivery Canceled successfully!" };
        }

        public async Task<Result> AsignDeliveryAsync(DeliveryDetailDTO model)
        {
            if (model == null) return new Result { IsSuccessful = false, Message = "Object can not be null" };
            //create Delivery
            var delivery = new DeliveryDetails(model.AppUserId, model.DeliveriesId);
            _unitOfWork.Repository<DeliveryDetails>().Add(delivery);
            //save delivery to get Id
            await _unitOfWork.Complete();
            return new Result { IsSuccessful = true, Message = "Delivery Asigned successfully!" };
        }

        public async Task<Result> GetDeliveryDetailsByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> CancelDeliveryByUserAsync(DeliveryDetailDTO model)
        {
            if (model == null) return new Result { IsSuccessful = false, Message = "Object can not be null" };
            //get delivery
            var delivery = await _unitOfWork.Repository<Delivery>().GetByIdAsync(model.DeliveriesId);
            if(delivery == null)
            {
                return new Result { IsSuccessful = false, Message = "delivery not found!" };
            }
            if (delivery != null)
            {
                //check if delivery has be assigned
                var spec = new DeliveryDetailsSpecification(delivery.Id);
                var deliverydetails = await _unitOfWork.Repository<DeliveryDetails>().GetEntityWithSpec(spec);
                if(deliverydetails != null) 
                {
                    return new Result { IsSuccessful = false, Message = "Delivery has been assigned!" };
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

                    //Create wallet tranaction
                    //generate transaction ref
                    var transactinref = _security.GetCode("WAL").ToUpper();
                    WalletTransaction transaction = new WalletTransaction(wallet.Id, Core.Enum.TransactionType.WalletTopUp, delivery.TotalAmount,
                    "Deposit", transactinref, Core.Enum.WalletTransactionStatus.Successful);
                    _unitOfWork.Repository<WalletTransaction>().Add(transaction);

                    //save changes to context
                     await _unitOfWork.Complete();
                }
            }

            return new Result { IsSuccessful = true, Message = "Delivery Canceled successfully!" };

        }
    }
}
