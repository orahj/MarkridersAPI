using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MarkriderAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeliveryController : ControllerBase
    {
        private readonly ILogger<DeliveryController> _logger;
        public IDeliveryRepository _repo { get; }

        public DeliveryController(ILogger<DeliveryController> logger, IDeliveryRepository repo)
        {
            _repo = repo;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetDeliveries()
        {
           var res = await _repo.GetDeliveryAsync();
           return Ok(res);
        }

         [HttpGet("{id}")]
        public async Task<ActionResult> GetDeliveryByID(int id)
        {
           var res = await _repo.GetDeliveryByIdAsync(id);
           return Ok(res);
        }
        [HttpGet("delivery-items")]
        public async Task<IActionResult> GetDeliveryItems()
        {
           var res = await _repo.GetDeliverItemsyAsync();
           return Ok(res);
        }

         [HttpGet("delivery-item/{id}")]
        public async Task<ActionResult> GetDeliveryItemByID(int id)
        {
           var res = await _repo.GetDeliveryItemByIdAsync(id);
           return Ok(res);
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

    }
}