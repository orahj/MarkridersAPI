using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Implementations
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly MarkRiderContext _context;
        public DeliveryRepository(MarkRiderContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<DeliveryItem>> GetDeliverItemsyAsync()
        {
            return await _context.DeliveryItems
            .Include(x =>x.Delivery)
            .Include(x =>x.DeliveryLocation)
            .Include(x =>x.FileData)
            .ToListAsync();
        }

        public async Task<IReadOnlyList<Delivery>> GetDeliveryAsync()
        {
           var deliveries = await _context.Deliveries
           .Include(x => x.DeliveryItems)
           .Include(x => x.AppUser)
           .ToListAsync();
          return deliveries;
        }

        public async Task<Delivery> GetDeliveryByIdAsync(int Id)
        {
             var delivery =  await _context.Deliveries
             .Include(x =>x.AppUser)
             .Include(x => x.DeliveryItems)
             .FirstOrDefaultAsync(x =>x.Id == Id);
             return delivery;
        }

        public async Task<IReadOnlyList<DeliveryDistance>> GetDeliveryDistanceAsync()
        {
            return await _context.DeliveryDistances.ToListAsync();
        }

        public async Task<DeliveryDistance> GetDeliveryDistanceByIdAsync(int Id)
        {
            return await _context.DeliveryDistances.FindAsync(Id);
        }

        public async Task<DeliveryItem> GetDeliveryItemByIdAsync(int Id)
        {
            return await _context.DeliveryItems
            .Include(x => x.FileData)
            .Include(x =>x.DeliveryLocation)
            .Include(x =>x.Delivery)
            .FirstOrDefaultAsync(x =>x.Id == Id);
        }

        public async Task<DeliveryLocation> GetDeliveryLocationByIdAsync(int Id)
        {
            return await _context.DeliveryLocations.FindAsync(Id);
        }

        public async Task<IReadOnlyList<DeliveryLocation>> GetDeliveryLocationsAsync()
        {
            return await _context.DeliveryLocations.ToListAsync();
        }
    }
}