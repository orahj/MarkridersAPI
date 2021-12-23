using Core.DTOs;
using Core.DTOs.Delivery;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IDeliveryDetailsRepository
    {
        Task<Result> AsignDeliveryAsync(DeliveryDetailDTO model);
        Task<Result> GetDeliveryDetailsByEmailAsync(string email);
        Task<Result> CancelDeliveryAsync(DeliveryDetailDTO model);
        Task<Result> CancelDeliveryByUserAsync(DeliveryDetailDTO model);
    }
}
