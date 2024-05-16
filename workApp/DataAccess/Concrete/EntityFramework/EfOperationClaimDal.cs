using Core.Entity.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;

namespace DataAccess.Concrete.EntityFramework;

public class EfOperationClaimDal : EfEntityRepositoryBase<OperationClaim,EfDbContext> ,IOperationClaimRepository
{
    
}