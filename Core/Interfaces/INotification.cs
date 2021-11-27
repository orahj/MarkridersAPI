using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs;
using Core.DTOs.Notification;

namespace Core.Interfaces
{
    public interface INotification
    {
        Task<Result> GetNotificationById(string id);
        Task<Result> GetNotificationsByDate(NotificationsByDateDto data);
        Task<Result> GetUserNotificationsByDate(UserNotificationsByDateDto data);
        Task<Result> GetUserNotifications(string email);
        Task<Result> ReadNotification(string id);
        Task<Result> MarkAllAsRead(string email);
        Task<Result> DeleteNotification(string id);
        Task<Result> DeleteAllNotifications(string email);
    }
}