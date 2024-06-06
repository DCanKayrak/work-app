using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using Entities.Concrete.Enums;

namespace DataAccess.Concrete.EntityFramework;

public class EfNotificationDal : EfEntityRepositoryBase<Notification, EfDbContext>, INotificationRepository
{
    
}