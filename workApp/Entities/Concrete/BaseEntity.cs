using Core.Entity.Abstract;

namespace Entities.Concrete;

public class BaseEntity : IEntity
{
    public int Id { get; set; }
    public DateTime Created_At { get; set; } = DateTime.UtcNow;
    public DateTime Updated_At { get; set; } = DateTime.UtcNow;
}