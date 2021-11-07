using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IDeliveryRepository
    {
        Task<Delivery> GetDeliveryByIdAsync(int Id);
        Task<IReadOnlyList<Delivery>> GetDeliveryAsync();
        Task<DeliveryItem> GetDeliveryItemByIdAsync(int Id);
        Task<IReadOnlyList<DeliveryItem>> GetDeliverItemsyAsync();
        Task<DeliveryLocation> GetDeliveryLocationByIdAsync(int Id);
        Task<IReadOnlyList<DeliveryLocation>> GetDeliveryLocationsAsync();
        Task<DeliveryDistance> GetDeliveryDistanceByIdAsync(int Id);
        Task<IReadOnlyList<DeliveryDistance>> GetDeliveryDistanceAsync();
    }
}