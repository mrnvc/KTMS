namespace KTMS.Application.Modules.Notifications.Dtos
{
    public class NotificationDTO
    {
        public required string Token { get; set; }
        public required string Title { get; set; }
        public required string Body { get; set; }
    }
}
