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
    public class RiderGuarantorRepository : IRiderGuarantorRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public RiderGuarantorRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public async Task<RiderGuarantor> CreateRiderGuarantorAsync(RiderGuarantorDTO model)
        {
           var riderGuarantor = new RiderGuarantor(model.RiderId);
            _unitOfWork.Repository<RiderGuarantor>().Add(riderGuarantor);
            //save delivery to get Id
            await _unitOfWork.Complete();
            return riderGuarantor;
        }

        public async Task<bool> UpdateRiderGuarantorAsync(RiderGuarantorDTO model)
        {
            var spec = new RIderGuarantorSpec(model.RiderId);
            var riderGuarantoInfo = await _unitOfWork.Repository<RiderGuarantor>().GetEntityWithSpec(spec);
            if(riderGuarantoInfo != null)
            {
                riderGuarantoInfo.FirstName = model.FirstName;
                riderGuarantoInfo.LastName = model.LastName;
                riderGuarantoInfo.NIN = model.NIN;
                _unitOfWork.Repository<RiderGuarantor>().Update(riderGuarantoInfo);
                 await _unitOfWork.Complete();
                return true;
            }
            return false;
        }
    }
}