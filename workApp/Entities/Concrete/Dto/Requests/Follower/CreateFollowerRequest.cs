using Core.Entity.Abstract;

namespace Entities.Concrete.Dto.Requests.Follower
{
    public class CreateFollowerRequest : IDto
    {
        public int To { get; set; }
    }   
}