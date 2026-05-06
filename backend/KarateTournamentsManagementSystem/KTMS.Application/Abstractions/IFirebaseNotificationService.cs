namespace KTMS.Application.Abstractions
{
    public interface IFirebaseNotificationService
    {
        Task<string> SendNotificationAsync(string token, string title, string body);
    }
}
