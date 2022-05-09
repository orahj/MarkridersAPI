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
        Task<Result> AsignDeliveryAsync(AsigndeliveryDTO model);
        Task<Result> GetDeliveryDetailsByEmailAsync(string userId);
        Task<Result> CancelDeliveryAsync(DeliveryDetailDTO model);
        Task<Result> CancelDeliveryByUserAsync(DeliveryDetailDTO model);
        Task<Result> CompletDeliveryAsync(DeliveryCompletionDTO model);
        Task<Result> DisputedDeliveryAsync(DeliveryDisputedDTO model);
        Task<Result> FulfilledDeliveryAsync(DeliverydeliveredDTO model);
        Task<Result> RidersalesAsync(string userId);
        Task<Result> TotalSalesAsync();
        Task<Result> StartDeliveryAsync(DeliverydeliveredDTO model);
    }
}
