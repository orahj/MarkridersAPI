using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IRiderRepository
    {
         Task<Rider> CreateRiderAsync(RiderDTO model);
          Task<bool> UpdateRiderAsync(RiderDTO model);
        Task<bool> ChangeStatus(RiderStatusDTO riderStatusDTO);
    }
}