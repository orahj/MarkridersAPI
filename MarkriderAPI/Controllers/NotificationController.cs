using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs.Notification;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarkriderAPI.Controllers
{
    public class NotificationController : BaseAPiController
    {
        private readonly INotification _notification;
        public NotificationController(INotification notification)
        {
            _notification = notification;
        }
        [HttpDelete("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteNotifications([FromQuery]string email)
        {
            var res = await _notification.DeleteAllNotifications(email);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            var res = await _notification.DeleteNotification(id);
            return Ok(res);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(NotificationResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNotification(int id)
        {
            var res = await _notification.GetNotificationById(id);
            return Ok(res);
        }

        //[HttpGet("notifications-by-date")]
        //[ProducesResponseType(typeof(List<NotificationResponseDto>), StatusCodes.Status200OK)]
        //public async Task<IActionResult> GetNotificationsByDate([FromBody] NotificationsByDateDto data)
        //{
        //    return Ok();
        //}

        [HttpGet("all")]
        [ProducesResponseType(typeof(List<NotificationResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserNotifications([FromQuery]string email)
        {
            var res = await _notification.GetUserNotifications(email);
            return Ok(res);
        }

        //[HttpGet("user-notifications-by-date")]
        //[ProducesResponseType(typeof(List<NotificationResponseDto>), StatusCodes.Status200OK)]
        //public async Task<IActionResult> GetUserNotificationsByDate([FromBody] UserNotificationsByDateDto data)
        //{
        //    return Ok();
        //}

        [HttpPatch("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> MarkAllAsRead([FromQuery]string email)
        {
            var res = await _notification.MarkAllAsRead(email);
            return Ok(res);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> ReadNotification(int id)
        {
            var res = await _notification.ReadNotification(id);
            return Ok(res);
        }
    }
}