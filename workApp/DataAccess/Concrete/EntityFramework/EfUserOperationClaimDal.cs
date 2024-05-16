using Core.Entity.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;

namespace DataAccess.Concrete.EntityFramework;

public class EfUserOperationClaimDal : EfEntityRepositoryBase<UserOperationClaim, EfDbContext>, IUserOperationClaimRepository
{
    
}