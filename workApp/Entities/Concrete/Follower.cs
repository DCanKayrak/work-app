using Core.Entity.Abstract;
using Entities.Concrete.Enums;

namespace Entities.Concrete;

public class Follower : BaseEntity, IEntity
{
    public int From { get; set; }
    public int To { get; set; }
    public FollowerStatus Status { get; set; } = FollowerStatus.Pending;
}