using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract;

public interface IFollowerService : ICrudService<Follower>
{
    IResult RespondRequest(int id, bool response);
}