using System.Linq.Expressions;
using Entities.Concrete;

namespace DataAccess.Abstract;

public interface ITaskCollectionRepository : IEntityRepository<TaskCollection>
{
}