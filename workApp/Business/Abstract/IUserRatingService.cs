using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract;

public interface IUserRatingService
{
    public IDataResult<UserRating> Get(int id);
    public IResult Create(UserRating userRating);
    public IResult Delete(int id);
    public IDataResult<UserRating> Update(UserRating userRating);
}