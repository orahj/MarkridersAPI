using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Enum;

namespace Core.DTOs.Notification
{
    public class NotificationResponseDto
    {
            public Guid Id { get; set; }
            public NotificationType Type { get; set; }
            public bool Read { get; set; }
            public DateTime DateCreated { get; set; }
            public Dictionary<string, string> Data { get; set; }
    }
}