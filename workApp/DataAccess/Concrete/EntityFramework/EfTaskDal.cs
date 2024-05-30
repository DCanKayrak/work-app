using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework;

public class EfTaskDal : EfEntityRepositoryBase<TaskItem,EfDbContext> ,ITaskRepository
{
    
}