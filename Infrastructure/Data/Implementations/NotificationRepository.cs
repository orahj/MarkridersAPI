using Core.DTOs;
using Core.DTOs.Notification;
using Core.Entities;
using Core.Entities.Identity;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Implementations
{
    public class NotificationRepository : INotification
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        public NotificationRepository(IUnitOfWork unitOfWork, UserManager<AppUser> userManager) 
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public async Task<Result> DeleteAllNotifications(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return new Result { IsSuccessful = false, Message = "User not found" };
            var spec = new NotificationSpec(user.Id.ToString());
            var notifications = await _unitOfWork.Repository<Notification>().ListAsync(spec);
            foreach (var notification in notifications)
            {
                _unitOfWork.Repository<Notification>().Delete(notification);
                await _unitOfWork.Complete();
            }
            return new Result { IsSuccessful = true, Message = "All notifications has been cleard." };
        }

        public async Task<Result> DeleteNotification(int id)
        {
            var notification = await _unitOfWork.Repository<Notification>().GetByIdAsync(id);
            _unitOfWork.Repository<Notification>().Delete(notification);
            return new Result { IsSuccessful = true, Message = "Notifications was deleted." };
        }

        public async Task<Result> GetNotificationById(int id)
        {
            var notification = await _unitOfWork.Repository<Notification>().GetByIdAsync(id);
            return new Result { IsSuccessful = true, ReturnedObject = notification };
        }

        public Task<Result> GetNotificationsByDate(NotificationsByDateDto data)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> GetUserNotifications(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return new Result { IsSuccessful = false, Message = "User not found" };
            var spec = new NotificationSpec(user.Id.ToString());
            var notification = await _unitOfWork.Repository<Notification>().ListAsync(spec);
            return new Result { IsSuccessful = true, ReturnedObject = notification };
        }

        public Task<Result> GetUserNotificationsByDate(UserNotificationsByDateDto data)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> MarkAllAsRead(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return new Result { IsSuccessful = false, Message = "User not found" };
            var spec = new NotificationSpec(user.Id.ToString());
            var notifications = await _unitOfWork.Repository<Notification>().ListAsync(spec);
            foreach (var notification in notifications)
            {
                notification.Read = true;
                _unitOfWork.Repository<Notification>().Update(notification);
                await _unitOfWork.Complete();
            }
            return new Result { IsSuccessful = true, Message = "All notifications have been marked as read." };
        }

        public async Task<Result> ReadNotification(int id)
        {
            var notification = await _unitOfWork.Repository<Notification>().GetByIdAsync(id);
            notification.Read = true;
            _unitOfWork.Repository<Notification>().Update(notification);
            await _unitOfWork.Complete();
            return new Result { IsSuccessful = true, Message = "Notification has been marked as read." };
        }
    }
}
