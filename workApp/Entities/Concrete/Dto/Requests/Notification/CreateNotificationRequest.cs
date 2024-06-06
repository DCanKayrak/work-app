using Core.Entity.Abstract;
using Entities.Concrete.Enums;

namespace Entities.Concrete.Dto.Requests.Notification;

public class CreateNotificationRequest : IDto
{
    public int UserId { get; set; }
    public string Message { get; set; }
    public bool IsRead { get; set; } = false;
    public NotificationType NotificationType { get; set; }

    public CreateNotificationRequest(int userId, string message, NotificationType notificationType)
    {
        UserId = userId;
        NotificationType = notificationType;
        Message = message;
    }
}