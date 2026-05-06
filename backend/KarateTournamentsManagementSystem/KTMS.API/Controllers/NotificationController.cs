using KTMS.Application.Abstractions;
using KTMS.Application.Modules.Notifications.Dtos;

namespace KTMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IFirebaseNotificationService _firebaseNotificationService;

        public NotificationController(IFirebaseNotificationService firebaseNotificationService)
        {
            _firebaseNotificationService = firebaseNotificationService;
        }

        [AllowAnonymous]
        [HttpPost("SendFirebaseNotification")]
        public async Task<IActionResult> SendFirebaseNotification([FromBody] NotificationDTO dto)
        {
            try
            {
                var result = await _firebaseNotificationService.SendNotificationAsync(
                    dto.Token,
                    dto.Title,
                    dto.Body
                );

                return Ok(new
                {
                    message = "Notification sent successfully.",
                    firebaseMessageId = result
                });
            }
            catch (FirebaseAdmin.Messaging.FirebaseMessagingException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }
    }
}
