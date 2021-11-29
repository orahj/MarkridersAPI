using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Core.DTOs;
using Core.DTOs.Delivery;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using MarkriderAPI.Controllers.DTOS;
using MarkriderAPI.Controllers.errors;
using MarkriderAPI.Extensions;
using MarkriderAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MarkriderAPI.Controllers
{
   //[Authorize]
    public class DeliveryController : BaseAPiController
    {
        private readonly IMapper _mapper;
        private readonly IDeliveryRepository _repo;

        public DeliveryController(IMapper mapper, IDeliveryRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Delivery>>> GetDeliveries(string sort, string email,[FromQuery] SpecParams specParams)
        {
           var res = await _repo.GetDeliveryAsync();
           //var totalItem = await _repo.GetCountAsync(sort,email,specParams);
           //var data = _mapper.Map<IReadOnlyList<Delivery>,IReadOnlyList<DeliveryDTO>>(res);
           return Ok(_mapper.Map<IReadOnlyList<Delivery>,IReadOnlyList<DeliveryReturnDTO>>(res));
        }

         [HttpGet("{id}")]
         [ProducesResponseType(StatusCodes.Status200OK)]
         [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

        public async Task<ActionResult<DeliveryReturnDTO>> GetDeliveryByID(int id)
        {
           var res = await _repo.GetDeliveryByIdAsync(id);
           if(res == null) return NotFound(new ApiResponse(404));
           return _mapper.Map<Delivery,DeliveryReturnDTO>(res);
        }
         [HttpGet("get-delivery-by-email")]
        public async Task<ActionResult<IReadOnlyList<DeliveryReturnDTO>>> GetDeliveriesByEmail()
        {
          var email = HttpContext.User.RetrieveEmailFromPrincipal();
           var res = await _repo.GetDeliveryByEmailAsync(email);
         //   var totalItem = await _repo.GetCountAsync(sort,email,specParams);
           //var data = _mapper.Map<IReadOnlyList<Delivery>,IReadOnlyList<DeliveryDTO>>(res);
           return Ok(_mapper.Map<IReadOnlyList<Delivery>,IReadOnlyList<DeliveryReturnDTO>>(res));
        }
         [HttpGet("get-delivery-by-shipment/{shipmentNo}")]
        public async Task<ActionResult<DeliveryReturnDTO>> GetDeliveriesNoEmail(string num)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();
           var res = await _repo.GetDeliveryByDeliveryNoAsync(email,num);
         //   var totalItem = await _repo.GetCountAsync(sort,email,specParams);
           //var data = _mapper.Map<IReadOnlyList<Delivery>,IReadOnlyList<DeliveryDTO>>(res);
           return Ok(_mapper.Map<Delivery,DeliveryReturnDTO>(res));
        }
        [HttpGet("delivery-items")]
        public async Task<ActionResult<IReadOnlyList<DeliveryItemReturnDTO>>> GetDeliveryItems()
        {
           var res = await _repo.GetDeliverItemsyAsync();
           return Ok(_mapper.Map<IReadOnlyList<DeliveryItem>,IReadOnlyList<DeliveryItemReturnDTO>>(res));
        }

        [HttpGet("delivery-item/{id}")]
        public async Task<ActionResult<DeliveryItemReturnDTO>> GetDeliveryItemByID(int id)
        {
           var res = await  _repo.GetDeliveryItemByIdAsync(id);
           return _mapper.Map<DeliveryItem,DeliveryItemReturnDTO>(res);
        }
        [HttpGet("delivery-item-by-delivery/{deliveryId}/{id}")]
        public async Task<ActionResult<DeliveryItemReturnDTO>> GetDeliveryItemByDeliveryID(int deliveryId,int id)
        {
           var res = await  _repo.GetDeliveryItemByDeliveryIdAsync(deliveryId,id);
           return _mapper.Map<DeliveryItem,DeliveryItemReturnDTO>(res);
        }
        [HttpGet("delivery-locations")]
        public async Task<IActionResult> GetDeliveryLocations()
        {
           var res = await _repo.GetDeliveryLocationsAsync();
           return Ok(res);
        }

         [HttpGet("delivery-location/{id}")]
        public async Task<ActionResult> GetDeliveryLocationsByID(int id)
        {
           var res = await _repo.GetDeliveryLocationByIdAsync(id);
           return Ok(res);
        }
        [HttpGet("delivery-distance")]
        public async Task<IActionResult> GetDeliveryDistance()
        {
           var res = await _repo.GetDeliveryDistanceAsync();
           return Ok(res);
        }

        [HttpGet("delivery-distance/{id}")]
        public async Task<ActionResult> GetDeliveryDistanceByID(int id)
        {
           var res = await _repo.GetDeliveryDistanceByIdAsync(id);
           return Ok(res);
        }

        [Authorize]
        [HttpPost("create-delivery")]
        public async Task<ActionResult<Result>> CreateDelivery(DeliveryDTO deliveryDto)
        {
           var email = HttpContext.User.RetrieveEmailFromPrincipal();
            var delivery = await _repo.CreateDeliveryAsync(deliveryDto);
            if(delivery == null) return BadRequest(new ApiResponse(400,"Error occured while creating shipment"));

           return new Result{
              IsSuccessful = true,
              Message = "Delivery created successfully",
              ReturnedObject = _mapper.Map<Delivery,DeliveryReturnDTO>(delivery)
           };
        }
         [HttpGet("delivery-transactions/{id}")]
        public async Task<ActionResult> GetDeliveryTransactionsByID(int id)
        {
           var res = await _repo.GetDeliveryDistanceByIdAsync(id);
           return Ok(res);
        }
    }
}