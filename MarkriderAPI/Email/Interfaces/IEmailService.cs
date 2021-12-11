using Core.DTOs;
using Core.Entities.Identity;
using System.Threading.Tasks;

namespace MarkriderAPI.Email.Interfaces
{
    public interface IEmailService
    {
        Task<Result> SendWelcomeMailAsync(AppUser user, string link);
        Task<Result> SendPasswordResetMailAsync(AppUser user, string link, string helpUrl, string operatingSystem, string browserName);
    }
}
