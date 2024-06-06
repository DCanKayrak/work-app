using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework;

public class EfUserRatingDal : EfEntityRepositoryBase<UserRating, EfDbContext>, IUserRatingRepository
{
    
}