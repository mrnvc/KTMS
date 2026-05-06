using FirebaseAdmin.Messaging;
using KTMS.Application.Abstractions;

namespace KTMS.API.Services
{
    public class FirebaseNotificationService : IFirebaseNotificationService
    {
        public async Task<string> SendNotificationAsync(string token, string title, string body)
        {
            var message = new Message
            {
                Token = token,
                Notification = new Notification
                {
                    Title = title,
                    Body = body
                }
            };

            return await FirebaseMessaging.DefaultInstance.SendAsync(message);
        }
    }
}