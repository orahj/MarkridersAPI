using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using MarkriderAPI.Controllers.DTOS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MarkriderAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeliveryController : ControllerBase
    {
        private readonly IGenericRepository<Delivery> _deliveryRepo;
        private readonly IGenericRepository<DeliveryDistance> _deliveryDistanceRepo;
        private readonly IGenericRepository<DeliveryItem> _deliveryItemRepo;
        private readonly IGenericRepository<DeliveryLocation> _deliveryLocationRepo;
        private readonly IMapper _mapper;

        public DeliveryController(IGenericRepository<Delivery> deliveryRepo,IGenericRepository<DeliveryDistance> deliveryDistanceRepo,
        IGenericRepository<DeliveryItem> deliveryItemRepo, IGenericRepository<DeliveryLocation> deliveryLocationRepo,IMapper mapper)
        {
            _mapper = mapper;
            _deliveryLocationRepo = deliveryLocationRepo;
            _deliveryItemRepo = deliveryItemRepo;
            _deliveryDistanceRepo = deliveryDistanceRepo;
            _deliveryRepo = deliveryRepo;
            
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<DeliveryDto>>> GetDeliveries()
        {
           var spec = new DeliverySpecification();

           var res = await _deliveryRepo.ListAsync(spec);
            // return res.Select(res => new DeliveryDto
            // {
            //    DeliveryNo = res.DeliveryNo,
            //    AppUserId = res.AppUserId,
            //    TotalAmount = res.TotalAmount,
            //    DateCreated = res.DateCreated,
            //    UserName = res.AppUser.UserName,
            //    FirstName = res.AppUser.FirstName,
            //    LastName = res.AppUser.LastName,
            //    Address = res.AppUser.Address,
            //    Avatar = res.AppUser.Avatar
            // }).ToList();
           return Ok(_mapper.Map<IReadOnlyList<Delivery>,IReadOnlyList<DeliveryDto>>(res));
        }

         [HttpGet("{id}")]
        public async Task<ActionResult<DeliveryDto>> GetDeliveryByID(int id)
        {
           var spec = new DeliverySpecification(id);

           var res = await _deliveryRepo.GetEntityWithSpec(spec);

           return _mapper.Map<Delivery,DeliveryDto>(res);
        }
        [HttpGet("delivery-items")]
        public async Task<ActionResult<IReadOnlyList<DeliveryItemDto>>> GetDeliveryItems()
        {
            var spec = new DeliveryItemSpecification();
           var res = await _deliveryItemRepo.ListAsync(spec);
           return Ok(_mapper.Map<IReadOnlyList<DeliveryItem>,IReadOnlyList<DeliveryItemDto>>(res));
        }

         [HttpGet("delivery-item/{id}")]
        public async Task<ActionResult<DeliveryItemDto>> GetDeliveryItemByID(int id)
        {
           var spec = new DeliveryItemSpecification(id);
           var res = await  _deliveryItemRepo.GetEntityWithSpec(spec);
           return _mapper.Map<DeliveryItem,DeliveryItemDto>(res);
        }
        [HttpGet("delivery-locations")]
        public async Task<IActionResult> GetDeliveryLocations()
        {
           var res = await _deliveryLocationRepo.ListAllAsync();
           return Ok(res);
        }

         [HttpGet("delivery-location/{id}")]
        public async Task<ActionResult> GetDeliveryLocationsByID(int id)
        {
           var res = await _deliveryLocationRepo.GetByIdAsync(id);
           return Ok(res);
        }
        [HttpGet("delivery-distance")]
        public async Task<IActionResult> GetDeliveryDistance()
        {
           var res = await _deliveryDistanceRepo.ListAllAsync();
           return Ok(res);
        }

         [HttpGet("delivery-distance/{id}")]
        public async Task<ActionResult> GetDeliveryDistanceByID(int id)
        {
           var res = await _deliveryDistanceRepo.GetByIdAsync(id);
           return Ok(res);
        }

    }
}