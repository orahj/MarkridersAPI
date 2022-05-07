using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Implementations
{
    public class RiderRepository : IRiderRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly MarkRiderContext _context;
        public RiderRepository(IUnitOfWork unitOfWork, MarkRiderContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<bool> ChangeStatus(RiderStatusDTO riderStatusDTO)
        {
            var spec = new RiderSpec(riderStatusDTO.UserId);
            var riderInfo = await _unitOfWork.Repository<Rider>().GetEntityWithSpec(spec);
            if (riderInfo == null) return false;

            riderInfo.RiderStatus = riderStatusDTO.Status;
            _unitOfWork.Repository<Rider>().Update(riderInfo);
            await _unitOfWork.Complete();
            return true;

        }

        public async Task<Rider> CreateRiderAsync(RiderDTO model)
        {
            var rider = new Rider(model.AppUserId);
             _unitOfWork.Repository<Rider>().Add(rider);
            //save delivery to get Id
            await _unitOfWork.Complete();
            return rider;
        }

        public async Task<Result> GetListOfRiders()
        {
            var riders = await _context.Riders.Include(x=>x.AppUser).ToListAsync();
            var ridersDto = new List<RiderDetailsDTO>();
            riders.ForEach((rider) => {
                var riderDto = new RiderDetailsDTO
                {
                    AccountNumber = rider.AccountNumber,
                    AppUserId = rider.AppUserId,
                    BankCode = rider.BankCode,
                    BVN = rider.BVN,
                    ValidID = rider.ValidID,
                    Id = rider.Id,
                    FirstName = rider.AppUser.FirstName,
                    LastName = rider.AppUser.LastName
                };

                ridersDto.Add(riderDto);
            });

            return new Result { IsSuccessful = true, ReturnedObject = ridersDto };
        }

        public async Task<Result> GetRiderIs(string Id)
        {
            var spec = new RiderSpec(Id);
            var riderInfo = await _unitOfWork.Repository<Rider>().GetEntityWithSpec(spec);
            if(riderInfo != null)
            {
                return new Result { IsSuccessful = true, Message = riderInfo.Id.ToString() };
            }
            return new Result { IsSuccessful = false, Message = "Not a rider" };
        }

        public async Task<bool> UpdateRiderAsync(RiderDTO model)
        {
            var spec = new RiderSpec(model.AppUserId);
            var riderInfo = await _unitOfWork.Repository<Rider>().GetEntityWithSpec(spec);
            if(riderInfo != null)
            {
                riderInfo.AccountNumber = model.AccountNumber;
                riderInfo.BankCode = model.BankCode;
                riderInfo.BVN = model.BVN;
                riderInfo.ValidID = model.ValidID;
                _unitOfWork.Repository<Rider>().Update(riderInfo);
                 await _unitOfWork.Complete();
                return true;
            }
            return false;
        }
    }
}