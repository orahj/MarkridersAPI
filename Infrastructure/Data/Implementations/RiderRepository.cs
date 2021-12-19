using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;

namespace Infrastructure.Data.Implementations
{
    public class RiderRepository : IRiderRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public RiderRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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