using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs.Notification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarkriderAPI.Controllers
{
    [Authorize]
    public class NotificationController : BaseAPiController
    {
         [HttpDelete("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteNotifications([FromQuery]string email)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteNotification(string id)
        {
            return Ok();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(NotificationResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNotification(string id)
        {
            return Ok();
        }

        [HttpGet("notifications-by-date")]
        [ProducesResponseType(typeof(List<NotificationResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNotificationsByDate([FromBody] NotificationsByDateDto data)
        {
            return Ok();
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(List<NotificationResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserNotifications([FromQuery]string email)
        {
            return Ok();
        }

        [HttpGet("user-notifications-by-date")]
        [ProducesResponseType(typeof(List<NotificationResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserNotificationsByDate([FromBody] UserNotificationsByDateDto data)
        {
            return Ok();
        }

        [HttpPatch("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> MarkAllAsRead([FromQuery]string email)
        {
           return Ok();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> ReadNotification(string id)
        {
            return Ok();
        }
    }
}