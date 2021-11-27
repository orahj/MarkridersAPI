using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IRiderGuarantorRepository
    {
         Task<RiderGuarantor> CreateRiderGuarantorAsync(RiderGuarantorDTO model);
         Task<bool> UpdateRiderGuarantorAsync(RiderGuarantorDTO model);
    }
}