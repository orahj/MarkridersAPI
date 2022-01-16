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
using Core.Entities.Identity;
using Core.Interfaces;
using Core.Specifications;
using MarkriderAPI.Controllers.DTOS;
using MarkriderAPI.Controllers.errors;
using MarkriderAPI.Extensions;
using MarkriderAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MarkriderAPI.Controllers
{
   //[Authorize]
    public class DeliveryController : BaseAPiController
    {
        private readonly IMapper _mapper;
        private readonly IDeliveryRepository _repo;
        private readonly IDeliveryDetailsRepository _deliveryDetailsRepository;
        private readonly UserManager<AppUser> _userManager;
        public DeliveryController(IMapper mapper, IDeliveryRepository repo, IDeliveryDetailsRepository deliveryDetailsRepository, UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _repo = repo;
            _deliveryDetailsRepository = deliveryDetailsRepository;
            _userManager = userManager;
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
         [HttpGet("get-delivery-by-email/{email}")]
        public async Task<ActionResult<IReadOnlyList<DeliveryReturnDTO>>> GetDeliveriesByEmail(string email)
        {
            //var email = HttpContext.User.RetrieveEmailFromPrincipal();
            var userEmail = await _userManager.FindByEmailAsync(email);
            if (userEmail == null)
            {
                return NotFound(new ApiResponse(404));
            }
            var res = await _repo.GetDeliveryByEmailAsync(userEmail.Email);
         //   var totalItem = await _repo.GetCountAsync(sort,email,specParams);
           //var data = _mapper.Map<IReadOnlyList<Delivery>,IReadOnlyList<DeliveryDTO>>(res);
           return Ok(_mapper.Map<IReadOnlyList<Delivery>,IReadOnlyList<DeliveryReturnDTO>>(res));
        }
         [HttpGet("get-delivery-by-shipment/{shipmentNo}/{email}")]
        public async Task<ActionResult<DeliveryReturnDTO>> GetDeliveriesNoEmail(string shipmentNo, string email)
        {
            //var email = HttpContext.User.RetrieveEmailFromPrincipal();
            var userEmail = await _userManager.FindByEmailAsync(email);
            if (userEmail == null)
            {
                return NotFound(new ApiResponse(404));
            }
            var res = await _repo.GetDeliveryByDeliveryNoAsync(userEmail.Email, shipmentNo);
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
        [HttpGet("delivery-items-by-delivery-id/{id}")]
        public async Task<ActionResult<IReadOnlyList<DeliveryItemReturnDTO>>> GetDeliveryItemsBydelivery(int id)
        {
            var res = await _repo.GetDeliverItemsybyDeliveryAsync(id);
            return Ok(_mapper.Map<IReadOnlyList<DeliveryItem>, IReadOnlyList<DeliveryItemReturnDTO>>(res));
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

        [HttpPost("create-delivery")]
        public async Task<ActionResult<Result>> CreateDelivery(DeliveryDTO deliveryDto)
        {
            //var email = HttpContext.User.RetrieveEmailFromPrincipal();
            var userEmail = await _userManager.FindByEmailAsync(deliveryDto.Email);
            if (userEmail == null)
            {
                return NotFound(new ApiResponse(404));
            }
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
        [HttpPost("cancel-delivery")]
        public async Task<ActionResult<Result>> CancelDelivery(DeliveryDetailDTO deliveryDto)
        {
            var delivery = await _deliveryDetailsRepository.CancelDeliveryAsync(deliveryDto);
            if (delivery == null) return BadRequest(new ApiResponse(400, "Error occured while canceling delivery"));

            return new Result
            {
                IsSuccessful = true,
                Message = "Delivery canceled successfully"
            };
        }
        [HttpPost("asign-delivery")]
        public async Task<ActionResult<Result>> AsignDelivery(AsigndeliveryDTO deliveryDto)
        {
            var delivery = await _deliveryDetailsRepository.AsignDeliveryAsync(deliveryDto);
            if (delivery == null) return BadRequest(new ApiResponse(400, "Error occured while asigning delivery"));

            return new Result
            {
                IsSuccessful = true,
                Message = "Delivery successfully asigned!"
            };
        }
        [HttpPost("cancel-delivery-by-user")]
        public async Task<ActionResult<Result>> CancelDeliveryByUser(DeliveryDetailDTO deliveryDto)
        {
            var delivery = await _deliveryDetailsRepository.CancelDeliveryByUserAsync(deliveryDto);
            if (delivery == null) return BadRequest(new ApiResponse(400, "Error occured while canceling delivery"));

            return new Result
            {
                IsSuccessful = true,
                Message = "Delivery canceled successfully"
            };
        }
        [HttpGet("get-rider-deliveries")]
        public async Task<ActionResult<Result>> GetRiderDeliveries([FromQuery]  string email)
        {
            var userEmail = await _userManager.FindByEmailAsync(email);
            if (userEmail == null)
            {
                return NotFound(new ApiResponse(404));
            }
            var delivery = await _deliveryDetailsRepository.GetDeliveryDetailsByEmailAsync(userEmail.Id.ToString());
            if (delivery == null) return BadRequest(new ApiResponse(400, "Error occured while getting deliveries"));

            return delivery;
        }
        [HttpGet("get-rider-sales-record")]
        public async Task<ActionResult<Result>> GetRiderSalesRecord([FromQuery] string email)
        {
            var userEmail = await _userManager.FindByEmailAsync(email);
            if (userEmail == null)
            {
                return NotFound(new ApiResponse(404));
            }
            var sales = await _deliveryDetailsRepository.RidersalesAsync(userEmail.Id.ToString());
            if (sales == null) return BadRequest(new ApiResponse(400, "Error occured while getting deliveries"));

            return sales;
        }
        [HttpPost("start-delivery")]
        public async Task<ActionResult<Result>> StartDelivery(DeliverydeliveredDTO deliveryDto)
        {
            var delivery = await _deliveryDetailsRepository.StartDeliveryAsync(deliveryDto);
            if (delivery == null) return BadRequest(new ApiResponse(400, "Error occured while canceling delivery"));

            return delivery;
        }
        [HttpPost("end-delivery")]
        public async Task<ActionResult<Result>> Delivereddelivery(DeliverydeliveredDTO deliveryDto)
        {
            var delivery = await _deliveryDetailsRepository.FulfilledDeliveryAsync(deliveryDto);
            if (delivery == null) return BadRequest(new ApiResponse(400, "Error occured while canceling delivery"));

            return delivery;
        }
        [HttpPost("completed-delivery")]
        public async Task<ActionResult<Result>> Completeddelvery(DeliveryCompletionDTO deliveryDto)
        {
            var userEmail = await _userManager.FindByIdAsync(deliveryDto.AppUserId);
            if (userEmail == null)
            {
                return NotFound(new ApiResponse(404));
            }
            deliveryDto.Email = userEmail.Email;
            var delivery = await _deliveryDetailsRepository.CompletDeliveryAsync(deliveryDto);
            if (delivery == null) return BadRequest(new ApiResponse(400, "Error occured while canceling delivery"));

            return delivery;
        }
        [HttpPost("disputed-delivery")]
        public async Task<ActionResult<Result>> DisputedDelivery(DeliveryDisputedDTO deliveryDto)
        {
            var delivery = await _deliveryDetailsRepository.DisputedDeliveryAsync(deliveryDto);
            if (delivery == null) return BadRequest(new ApiResponse(400, "Error occured while canceling delivery"));

            return delivery;
        }
        [HttpGet("get-cancellation-reason")]
        public async Task<ActionResult> GetcanellationReason()
        {
            var res = await _repo.GetCancelationReasonsAsync();
            return Ok(res);
        }
    }
}