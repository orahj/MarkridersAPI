using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs;
using Core.DTOs.Delivery;
using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
    public interface IDeliveryRepository
    {
        Task<Delivery> GetDeliveryByIdAsync(int Id);
        Task<IReadOnlyList<Delivery>> GetDeliveryAsync();
        Task<IReadOnlyList<Delivery>> GetDeliveryByEmailAsync(string email);
        Task<DeliveryItem> GetDeliveryItemByIdAsync(int Id);
        Task<IReadOnlyList<DeliveryItem>> GetDeliverItemsyAsync();
        Task<DeliveryLocation> GetDeliveryLocationByIdAsync(int Id);
        Task<IReadOnlyList<DeliveryLocation>> GetDeliveryLocationsAsync();
        Task<DeliveryDistance> GetDeliveryDistanceByIdAsync(int Id);
        Task<IReadOnlyList<DeliveryDistance>> GetDeliveryDistanceAsync();
        Task<Delivery> CreateDeliveryAsync(DeliveryDTO model);
        Task<int> GetCountAsync(string sort, string email,SpecParams specParams);
        Task<IReadOnlyList<Delivery>> GetDeliveryForUserAsync(string sort, string email, SpecParams specParams);
        Task<Delivery> GetDeliveryByDeliveryNoAsync(string email,string shipmentNo);

    }
}