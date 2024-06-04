using Entities.Concrete.Enums;

namespace Entities.Concrete;

public class Notification : BaseEntity
{
    public int UserId { get; set; }
    public string Message { get; set; }
    public bool IsRead { get; set; } = false;
    public NotificationType NotificationType { get; set; }
}