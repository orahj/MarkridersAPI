using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using MarkriderAPI.Controllers.errors;
using Microsoft.AspNetCore.Mvc;

namespace MarkriderAPI.Controllers
{
    [ApiExplorerSettings(IgnoreApi =true)]
    public class BuggyController : BaseAPiController
    {
        private readonly MarkRiderContext _context;
        public BuggyController(MarkRiderContext context)
        {
            _context = context;
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var item = _context.DeliveryItems.Find(45);

            if(item == null)
            return NotFound(new ApiResponse(404));

            return Ok();
        }
        [HttpGet("server-error")]
        public ActionResult GetServerError()
        {
             var item = _context.DeliveryItems.Find(45);

             var itemtoreturn = item.ToString();

            return Ok();
        }
        [HttpGet("bad-request")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }
        [HttpGet("bad-request/{id}")]
        public ActionResult GetNotFoundRequest(int id)
        {
            return Ok();
        }
    }
}