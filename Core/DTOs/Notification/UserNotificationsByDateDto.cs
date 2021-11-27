using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.DTOs.Notification
{
    public class UserNotificationsByDateDto
    {
        public string UserEmail { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
    }
}